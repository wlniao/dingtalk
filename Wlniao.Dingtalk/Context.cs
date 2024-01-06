using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Wlniao;
using Wlniao.Handler;

namespace Wlniao.Dingtalk
{
    /// <summary>
    /// 请求线程
    /// </summary>
    public class Context
    {
        /// <summary>
        /// API代理服务器地址
        /// </summary>
        public String Webroxy { get; set; }
        /// <summary>
        /// API访问令牌
        /// </summary>
        public String AccessToken { get; set; }
        /// <summary>
        /// API访问令牌Header参数名称
        /// </summary>
        public String AccessTokenHeaderName { get; set; }
        /// <summary>
        /// 当前请求Headers信息
        /// </summary>
        public Dictionary<String, String> Headers = new Dictionary<String, String>(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        /// 当前线程的配置信息
        /// </summary>
        public Dictionary<String, String> Configs = new Dictionary<String, String>(StringComparer.OrdinalIgnoreCase);


        /// <summary>
        /// 
        /// </summary>
        public Context(Dictionary<String, String> cfgs = null)
        {
            if (cfgs != null)
            {
                foreach (var item in cfgs)
                {
                    if (item.Key == "Webroxy")
                    {
                        this.Webroxy = item.Value;
                    }
                    else if (item.Key == "AccessToken")
                    {
                        this.AccessToken = item.Value;
                    }
                    else if (item.Key == "AccessToken")
                    {
                        this.AccessToken = item.Value;
                    }
                    else if (Configs.ContainsKey(item.Key))
                    {
                        Configs[item.Key] = item.Value;
                    }
                    else
                    {
                        Configs.TryAdd(item.Key, item.Value);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public Context(String accessToken, String accessTokenHeaderName = null)
        {
            this.AccessToken = accessToken;
            this.AccessTokenHeaderName = accessTokenHeaderName;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="host"></param>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <param name="accessToken">0:无须Token 1:位于Query参数 2:位于Header参数</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ResponseException"></exception>
        public ApiResult<T> Request<T>(string host, string path, object data = null, int accessToken = 2)
        {
            var logs = "";
            var start = DateTime.Now;
            var result = new ApiResult<T> { code = "", message = "未知错误" };
            var apiurl = (string.IsNullOrEmpty(Webroxy) ? host : Webroxy) + path;
            var headers = new Dictionary<String, String>(StringComparer.OrdinalIgnoreCase);
            try
            {
                if (accessToken > 0 && string.IsNullOrEmpty(this.AccessToken))
                {
                    result.message = "调用失败，请检查AccessToken是否正确生成";
                    return result;
                }
                else if (accessToken == 1)
                {
                    if (apiurl.IndexOf('?') < 0)
                    {
                        apiurl += "?access_token=" + AccessToken;
                    }
                    else
                    {
                        apiurl += "&access_token=" + AccessToken;
                    }
                }
                else if (accessToken == 2)
                {
                    if (string.IsNullOrEmpty(this.AccessTokenHeaderName))
                    {
                        headers.Add(Settings.AccessTokenHeaderName, AccessToken);
                    }
                    else
                    {
                        headers.Add(this.AccessTokenHeaderName, AccessToken);
                    }
                }
                foreach (var header in this.Headers)
                {
                    headers.TryAdd(header.Key, header.Value);
                }
                var handler = new HttpClientHandler { ServerCertificateCustomValidationCallback = XCore.ServerCertificateCustomValidationCallback };
                using (var client = new System.Net.Http.HttpClient(handler))
                {
                    var request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, apiurl);
                    if (data != null)
                    {
                        var req = System.Text.Json.JsonSerializer.Serialize(data, new JsonSerializerOptions
                        {
                            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) //Json序列化的时候对中文进行处理
                        });
                        if (Wlniao.Log.Loger.LogLevel <= Wlniao.Log.LogLevel.Information)
                        {
                            log.Topic("Dingtalk", path + " request:" + req);
                        }
                        logs += "\n >>> " + req;
                        request.Method = System.Net.Http.HttpMethod.Post;
                        request.Content = new System.Net.Http.StreamContent(cvt.ToStream(System.Text.Encoding.UTF8.GetBytes(req)));
                        request.Content.Headers.Add("Content-Type", "application/json");
                    }
                    if (string.IsNullOrEmpty(this.Webroxy))
                    {
                        this.Webroxy = Settings.Webroxy;
                    }
                    if (!string.IsNullOrEmpty(Webroxy))
                    {
                        host = host.Substring(host.IndexOf("://") + 3).Trim('/').Trim();
                        request.Headers.TryAddWithoutValidation("X-Webroxy", host);
                    }
                    foreach (var header in headers)
                    {
                        request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                    }
                    client.SendAsync(request).ContinueWith((requestTask) =>
                    {
                        var response = requestTask.Result;
                        response.Content.ReadAsStringAsync().ContinueWith((readTask) =>
                        {
                            var res = readTask.Result;
                            try
                            {
                                if (string.IsNullOrEmpty(res))
                                {
                                    throw new Exceptions.ResponseException("未获取到API接口输出内容");
                                }
                                else
                                {
                                    logs += "\n <<< " + res;
                                    if (Wlniao.Log.Loger.LogLevel <= Wlniao.Log.LogLevel.Information)
                                    {
                                        log.Topic("Dingtalk", path + " response:" + res);
                                    }
                                    try
                                    {
                                        var obj = System.Text.Json.JsonSerializer.Deserialize<Dictionary<String, Object>>(res, new System.Text.Json.JsonSerializerOptions { });
                                        result.code = obj.GetString("code", obj.GetString("errcode"));
                                        result.message = obj.GetString("message", obj.GetString("errmsg"));
                                        if (string.IsNullOrEmpty(result.code) || result.code == "0")
                                        {
                                            if (obj.ContainsKey("errcode") && obj.ContainsKey("result"))
                                            {
                                                res = System.Text.Json.JsonSerializer.Serialize(obj.GetValue("result"), new JsonSerializerOptions { Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) });
                                            }
                                            result.data = System.Text.Json.JsonSerializer.Deserialize<T>(res, new JsonSerializerOptions { Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) });
                                            result.code = result.data == null ? "-1" : "0";
                                            result.success = result.data == null ? false : true;
                                            result.message = result.data == null ? "接口调用失败" : "接口调用成功";
                                        }
                                    }
                                    catch { }
                                }
                            }
                            catch
                            {
                                throw new Exceptions.ResponseException("API输出内容反序列化失败");
                            }
                        }).Wait();
                    }).Wait();
                }
            }
            catch (Exception ex)
            {
                logs += "\n <<< " + ex.Message;
                if (Wlniao.Log.Loger.LogLevel <= Wlniao.Log.LogLevel.Information)
                {
                    log.Topic("Dingtalk", path + " exception:" + ex.Message);
                }
            }
            log.Debug("[Dingtalk][" + path + "][" + DateTime.Now.Subtract(start).TotalMilliseconds.ToString("F2") + "ms]" + logs);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exceptions.ConfigException"></exception>
        public ApiResult<Tuple<String, Int32>> GetAppToken()
        {
            var path = "";
            var data = new Dictionary<String, String>();
            var suiteKey = Configs.GetString("suiteKey");
            var suiteSecret = Configs.GetString("suiteSecret");
            var suiteTicket = Configs.GetString("suiteTicket");
            var authCorpId = Configs.GetString("authCorpId");
            var result = new ApiResult<Tuple<String, Int32>>();
            if (string.IsNullOrEmpty(suiteKey) || string.IsNullOrEmpty(suiteSecret) || string.IsNullOrEmpty(suiteTicket) || string.IsNullOrEmpty(authCorpId))
            {
                //走内部应用流程
                var appKey = Configs.GetString("appKey");
                var appSecret = Configs.GetString("appSecret");
                if (string.IsNullOrEmpty(appKey))
                {
                    result.message = "配置内容，缺少参数:appKey";
                }
                else if (string.IsNullOrEmpty(appSecret))
                {
                    result.message = "配置内容，缺少参数:appSecret";
                }
                else
                {
                    path = "/v1.0/oauth2/accessToken";
                    data.Add("appKey", appKey);
                    data.Add("appSecret", appSecret);
                }
            }
            else
            {
                path = "/v1.0/oauth2/corpAccessToken";
                data.Add("suiteKey", suiteKey);
                data.Add("suiteSecret", suiteSecret);
                data.Add("suiteTicket", suiteTicket);
                data.Add("authCorpId", authCorpId);
            }
            if (!string.IsNullOrEmpty(path))
            {
                var res = Request<Dictionary<String, Object>>(Settings.DingtalkApi, path, data, 0);
                var accessToken = res.code != "0" ? "" : res.data.GetString("accessToken");
                var expireIn = string.IsNullOrEmpty(accessToken) ? 0 : res.data.GetInt32("expireIn");
                if (expireIn > 0)
                {
                    this.AccessToken = accessToken;
                    result.data = new Tuple<String, Int32>(accessToken, expireIn);
                    result.message = "AccessToken获取成功";
                    result.success = true;
                }
                else
                {
                    result.code = res.code;
                    result.message = res.message;
                }
            }
            return result;
        }

    }
}