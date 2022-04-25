using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Wlniao.Dingtalk
{
    /// <summary>
    /// 
    /// </summary>
    public class Common
    {
        /// <summary>
        /// 钉钉开放平台加解密方法
        /// </summary>
        public class Cryptography
        {
            /// <summary>
            /// 将和钉钉开放平台同步的消息体加密,返回加密Map
            /// </summary>
            /// <param name="appid"></param>
            /// <param name="token"></param>
            /// <param name="aeskey"></param>
            /// <param name="plaintext">传递的消息体明文</param>
            /// <param name="timestamp">时间戳</param>
            /// <returns></returns>
            public static Dictionary<String, String> getEncryptedMap(String appid, String token, String aeskey, String plaintext, String timestamp = null)
            {
                if (null == plaintext)
                {
                    throw new DingTalkEncryptException(DingTalkEncryptException.ENCRYPTION_PLAINTEXT_ILLEGAL);
                }
                else if (string.IsNullOrEmpty(timestamp))
                {
                    timestamp = DateTime.Now.Millisecond.ToString();
                }
                var nonce = Wlniao.strUtil.CreateRndStrE(16);
                var encrypt = AES_encrypt(aeskey, plaintext, appid, nonce);
                var signature = getSignature(token, timestamp, nonce, encrypt);
                var resultMap = new Dictionary<String, String>();
                resultMap["msg_signature"] = signature;
                resultMap["timeStamp"] = timestamp;
                resultMap["encrypt"] = encrypt;
                resultMap["nonce"] = nonce;
                return resultMap;
            }

            /// <summary>
            /// 密文解密，返回解密后的原文
            /// </summary>
            /// <param name="appid"></param>
            /// <param name="token"></param>
            /// <param name="aeskey"></param>
            /// <param name="nonce">随机串</param>
            /// <param name="timeStamp">时间戳</param>
            /// <param name="encryptMsg">密文</param>
            /// <param name="msgSignature">签名串</param>
            /// <returns></returns>
            public static String getDecryptMsg(String appid, String token, String aeskey, String nonce, String timeStamp, String encryptMsg, String msgSignature)
            {
                //校验签名
                String signature = getSignature(token, timeStamp, nonce, encryptMsg);
                if (!signature.Equals(msgSignature))
                {
                    throw new DingTalkEncryptException(DingTalkEncryptException.COMPUTE_SIGNATURE_ERROR);
                }
                return AES_decrypt(aeskey, encryptMsg, appid);
            }


            /// <summary>
            /// 对明文加密，返回加密后base64编码的字符串
            /// </summary>
            /// <param name="Key"></param>
            /// <param name="Input">需要加密的明文</param>
            /// <param name="AppId"></param>
            /// <param name="Nonce"></param>
            /// <returns></returns>
            public static String AES_encrypt(String Key, String Input, String AppId, String Nonce)
            {
                try
                {
                    byte[] aesKey = System.Convert.FromBase64String(Key + "=");
                    byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(Input);// Input.getBytes(CHARSET);
                    byte[] appidBytes = System.Text.Encoding.UTF8.GetBytes(AppId);// AppId.getBytes(CHARSET);
                    byte[] nonceBytes = System.Text.Encoding.UTF8.GetBytes(Nonce);// Nonce.getBytes(CHARSET);
                    byte[] lengthByte = Utils.int2Bytes(inputBytes.Length);

                    var bytestmp = new List<byte>();
                    bytestmp.AddRange(nonceBytes);
                    bytestmp.AddRange(lengthByte);
                    bytestmp.AddRange(inputBytes);
                    bytestmp.AddRange(appidBytes);
                    byte[] padBytes = PKCS7Padding.getPaddingBytes(bytestmp.Count);
                    bytestmp.AddRange(padBytes);
                    byte[] unencrypted = bytestmp.ToArray();

                    RijndaelManaged rDel = new RijndaelManaged();
                    rDel.Mode = CipherMode.CBC;
                    rDel.Padding = PaddingMode.Zeros;
                    rDel.Key = aesKey;
                    rDel.IV = aesKey.ToList().Take(16).ToArray();
                    ICryptoTransform cTransform = rDel.CreateEncryptor();
                    byte[] resultArray = cTransform.TransformFinalBlock(unencrypted, 0, unencrypted.Length);
                    return System.Convert.ToBase64String(resultArray, 0, resultArray.Length);


                    //Cipher cipher = Cipher.getInstance("AES/CBC/NoPadding");
                    //SecretKeySpec keySpec = new SecretKeySpec(aesKey, "AES");
                    //IvParameterSpec iv = new IvParameterSpec(aesKey, 0, 16);
                    //cipher.init(Cipher.ENCRYPT_MODE, keySpec, iv);
                    //byte[] encrypted = cipher.doFinal(unencrypted);
                    //String result = base64.encodeToString(encrypted);
                    //return result;
                }
                catch
                {
                    throw new DingTalkEncryptException(DingTalkEncryptException.COMPUTE_ENCRYPT_TEXT_ERROR);
                }
            }

            /// <summary>
            /// 对密文进行解密
            /// </summary>
            /// <param name="Key">解密得到的明文</param>
            /// <param name="Input">需要解密的密文</param>
            /// <param name="AppId"></param>
            /// <returns></returns>
            public static String AES_decrypt(String Key, String Input, String AppId)
            {
                byte[] aesKey = System.Convert.FromBase64String(Key + "=");
                byte[] toEncryptArray = System.Convert.FromBase64String(Input);
                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Mode = CipherMode.CBC;
                rDel.Padding = PaddingMode.Zeros;
                rDel.Key = aesKey;
                rDel.IV = aesKey.ToList().Take(16).ToArray();
                ICryptoTransform cTransform = rDel.CreateDecryptor();
                var originalArr = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                //return System.Text.UTF8Encoding.UTF8.GetString(resultArray);

                //// 设置解密模式为AES的CBC模式
                //Cipher cipher = Cipher.getInstance("AES/CBC/NoPadding");
                //SecretKeySpec keySpec = new SecretKeySpec(aesKey, "AES");
                //IvParameterSpec iv = new IvParameterSpec(Arrays.copyOfRange(aesKey, 0, 16));
                //cipher.init(Cipher.DECRYPT_MODE, keySpec, iv);
                //// 使用BASE64对密文进行解码
                //byte[] encrypted = Base64.decodeBase64(text);
                //// 解密
                //originalArr = cipher.doFinal(encrypted);


                var bytes = PKCS7Padding.removePaddingBytes(originalArr); // 去除补位字符
                var msgLength = Utils.bytes2int(bytes.Skip(16).Take(4).ToArray()); // 开头是16字节的随机字符串，17-20位是msg长度;
                //var fromCorpid = System.Text.UTF8Encoding.UTF8.GetString(bytes.Skip(20 + msgLength).ToArray());
                if (string.IsNullOrEmpty(AppId) || AppId == UTF8Encoding.UTF8.GetString(bytes.Skip(20 + msgLength).ToArray()))
                {
                    return UTF8Encoding.UTF8.GetString(bytes.Skip(20).Take(msgLength).ToArray());
                }
                else
                {
                    return "";
                }
            }

            /// <summary>
            /// 数字签名
            /// </summary>
            /// <param name="token">isv token</param>
            /// <param name="timestamp">时间戳</param>
            /// <param name="nonce">随机串</param>
            /// <param name="encrypt">加密文本</param>
            /// <returns></returns>
            public static String getSignature(String token, String timestamp, String nonce, String encrypt)
            {
                try
                {
                    Console.Out.WriteLine(encrypt);

                    String[] array = new String[] { token, timestamp, nonce, encrypt };
                    Array.Sort(array, StringComparer.Ordinal);
                    //var tmparray = array.ToList();
                    //tmparray.Sort(new JavaStringComper());
                    //array = tmparray.ToArray();
                    Console.Out.WriteLine("array:" + JsonSerializer.Serialize(array));
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < 4; i++)
                    {
                        sb.Append(array[i]);
                    }
                    String str = sb.ToString();
                    Console.Out.WriteLine(str);
                    //MessageDigest md = MessageDigest.getInstance("SHA-1");
                    //md.update(str.getBytes());
                    //byte[] digest = md.digest();
                    System.Security.Cryptography.SHA1 hash = System.Security.Cryptography.SHA1.Create();
                    System.Text.Encoding encoder = System.Text.Encoding.ASCII;
                    byte[] combined = encoder.GetBytes(str);
                    byte[] digest = hash.ComputeHash(combined);
                    StringBuilder hexstr = new StringBuilder();
                    String shaHex = "";
                    for (int i = 0; i < digest.Length; i++)
                    {
                        shaHex = ((int)digest[i]).ToString("x");// Integer.toHexString(digest[i] & 0xFF);
                        if (shaHex.Length < 2)
                        {
                            hexstr.Append(0);
                        }
                        hexstr.Append(shaHex);
                    }
                    return hexstr.ToString();
                }
                catch
                {
                    throw new DingTalkEncryptException(DingTalkEncryptException.COMPUTE_SIGNATURE_ERROR);
                }
            }
        }


        /// <summary>
        /// 钉钉开放平台加解密异常类
        /// </summary>
        public class DingTalkEncryptException : Exception
        {
            /**成功**/
            public static readonly int SUCCESS = 0;
            /**加密明文文本非法**/
            public readonly static int ENCRYPTION_PLAINTEXT_ILLEGAL = 900001;
            /**加密时间戳参数非法**/
            public readonly static int ENCRYPTION_TIMESTAMP_ILLEGAL = 900002;
            /**加密随机字符串参数非法**/
            public readonly static int ENCRYPTION_NONCE_ILLEGAL = 900003;
            /**不合法的aeskey**/
            public readonly static int AES_KEY_ILLEGAL = 900004;
            /**签名不匹配**/
            public readonly static int SIGNATURE_NOT_MATCH = 900005;
            /**计算签名错误**/
            public readonly static int COMPUTE_SIGNATURE_ERROR = 900006;
            /**计算加密文字错误**/
            public readonly static int COMPUTE_ENCRYPT_TEXT_ERROR = 900007;
            /**计算解密文字错误**/
            public readonly static int COMPUTE_DECRYPT_TEXT_ERROR = 900008;
            /**计算解密文字长度不匹配**/
            public readonly static int COMPUTE_DECRYPT_TEXT_LENGTH_ERROR = 900009;
            /**计算解密文字corpid不匹配**/
            public readonly static int COMPUTE_DECRYPT_TEXT_CORPID_ERROR = 900010;

            private static Dictionary<int, String> msgMap = new Dictionary<int, String>();
            static DingTalkEncryptException()
            {
                msgMap[SUCCESS] = "成功";
                msgMap[ENCRYPTION_PLAINTEXT_ILLEGAL] = "加密明文文本非法";
                msgMap[ENCRYPTION_TIMESTAMP_ILLEGAL] = "加密时间戳参数非法";
                msgMap[ENCRYPTION_NONCE_ILLEGAL] = "加密随机字符串参数非法";
                msgMap[SIGNATURE_NOT_MATCH] = "签名不匹配";
                msgMap[COMPUTE_SIGNATURE_ERROR] = "签名计算失败";
                msgMap[AES_KEY_ILLEGAL] = "不合法的aes key";
                msgMap[COMPUTE_ENCRYPT_TEXT_ERROR] = "计算加密文字错误";
                msgMap[COMPUTE_DECRYPT_TEXT_ERROR] = "计算解密文字错误";
                msgMap[COMPUTE_DECRYPT_TEXT_LENGTH_ERROR] = "计算解密文字长度不匹配";
                msgMap[COMPUTE_DECRYPT_TEXT_CORPID_ERROR] = "计算解密文字corpid不匹配";
            }

            private int code;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="exceptionCode"></param>
            public DingTalkEncryptException(int exceptionCode) : base(msgMap[exceptionCode])
            {
                this.code = exceptionCode;
            }
        }

        /// <summary>
        /// PKCS7算法的加密填充
        /// </summary>
        public class PKCS7Padding
        {
            //private readonly static Charset CHARSET = Charset.forName("utf-8");
            private readonly static int BLOCK_SIZE = 32;

            /**
             * 填充mode字节
             * @param count
             * @return
             */
            public static byte[] getPaddingBytes(int count)
            {
                int amountToPad = BLOCK_SIZE - (count % BLOCK_SIZE);
                if (amountToPad == 0)
                {
                    amountToPad = BLOCK_SIZE;
                }
                char padChr = chr(amountToPad);
                String tmp = string.Empty; ;
                for (int index = 0; index < amountToPad; index++)
                {
                    tmp += padChr;
                }
                return System.Text.Encoding.UTF8.GetBytes(tmp);
            }

            /**
             * 移除mode填充字节
             * @param decrypted
             * @return
             */
            public static byte[] removePaddingBytes(byte[] decrypted)
            {
                int pad = (int)decrypted[decrypted.Length - 1];
                if (pad < 1 || pad > BLOCK_SIZE)
                {
                    pad = 0;
                }
                //Array.Copy()
                var output = new byte[decrypted.Length - pad];
                Array.Copy(decrypted, output, decrypted.Length - pad);
                return output;
            }

            private static char chr(int a)
            {
                byte target = (byte)(a & 0xFF);
                return (char)target;
            }

        }


        /// <summary>
        /// 加解密工具类
        /// </summary>
        public class Utils
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="count"></param>
            /// <returns></returns>
            public static String getRandomStr(int count)
            {
                String baset = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                Random random = new Random();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < count; i++)
                {
                    int number = random.Next(baset.Length);
                    sb.Append(baset[number]);
                }
                return sb.ToString();
            }

            /// <summary>
            /// int转byte数组,高位在前
            /// </summary>
            /// <param name="count"></param>
            /// <returns></returns>
            public static byte[] int2Bytes(int count)
            {
                byte[] byteArr = new byte[4];
                byteArr[3] = (byte)(count & 0xFF);
                byteArr[2] = (byte)(count >> 8 & 0xFF);
                byteArr[1] = (byte)(count >> 16 & 0xFF);
                byteArr[0] = (byte)(count >> 24 & 0xFF);
                return byteArr;
            }
            /// <summary>
            /// 高位在前bytes数组转int
            /// </summary>
            /// <param name="byteArr"></param>
            /// <returns></returns>
            public static int bytes2int(byte[] byteArr)
            {
                int count = 0;
                for (int i = 0; i < 4; ++i)
                {
                    count <<= 8;
                    count |= byteArr[i] & 255;
                }
                return count;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public class JavaStringComper : IComparer<string>
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public int Compare(string x, string y)
            {
                return String.Compare(x, y);
            }
        }
    }
}
