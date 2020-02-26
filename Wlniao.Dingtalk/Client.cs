using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wlniao.Dingtalk.Request;
using Wlniao.Dingtalk.Response;
namespace Wlniao.Dingtalk
{
    /// <summary>
    /// 企业微信内部开发客户端
    /// </summary>
    public class Client : Wlniao.Handler.IClient
    {
        #region 企业内部开发配置信息
        internal static string _CorpId = null;      //企业团体Id
        internal static string _AppKey = null;      //企业内部应用Id
        internal static string _AppSecret = null;   //企业内部应用开发密钥
        internal static string _ApiServer = null;   //服务器地址
        /// <summary>
        /// 企业团体Id
        /// </summary>
        public static string CfgCorpId
        {
            get
            {
                if (_CorpId == null)
                {
                    _CorpId = Config.GetSetting("DingCorpId");
                }
                return _CorpId;
            }
        }
        /// <summary>
        /// 企业内部应用Id
        /// </summary>
        public static string CfgAppKey
        {
            get
            {
                if (_AppKey == null)
                {
                    _AppKey = Config.GetSetting("DingAppKey");
                }
                return _AppKey;
            }
        }
        /// <summary>
        /// 企业内部应用开发密钥
        /// </summary>
        public static string CfgAppSecret
        {
            get
            {
                if (_AppSecret == null)
                {
                    _AppSecret = Config.GetSetting("DingAppSecret");
                }
                return _AppSecret;
            }
        }
        /// <summary>
        /// 钉钉API服务器地址
        /// </summary>
        public static string CfgApiServer
        {
            get
            {
                if (_ApiServer == null)
                {
                    _ApiServer = Config.GetSetting("DingApiServer");
                }
                if (string.IsNullOrEmpty(_ApiServer))
                {
                    _ApiServer = "https://oapi.dingtalk.com";
                }
                return _ApiServer;
            }
        }
        #endregion

        /// <summary>
        /// 企业团体Id
        /// </summary>
        public string CorpId { get; set; }
        /// <summary>
        /// 企业内部应用Id
        /// </summary>
        public string AppKey { get; set; }
        /// <summary>
        /// 企业内部开发密钥
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        /// 钉钉推送的SuiteTicket
        /// </summary>
        public string SuiteTicket { get; set; }
        /// <summary>
        /// 钉钉API服务器地址
        /// </summary>
        public string ApiServer { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Wlniao.Handler.PipelineHandler handler = null;
        /// <summary>
        /// 
        /// </summary>
        public Client()
        {
            this.CorpId = CfgCorpId;
            this.AppKey = CfgAppKey;
            this.AppSecret = CfgAppSecret;
            this.ApiServer = CfgApiServer;
            handler = new Handler();
        }
        /// <summary>
        /// 
        /// </summary>
        public Client(String AppKey, String AppSecret, String ApiServer = null)
        {
            this.AppKey = AppKey;
            this.AppSecret = AppSecret;
            this.ApiServer = string.IsNullOrEmpty(ApiServer) ? CfgApiServer : ApiServer;
            handler = new Handler();
        }
        /// <summary>
        /// 
        /// </summary>
        public Client(String CorpId, String AppKey, String AppSecret, String SuiteTicket, String ApiServer = null)
        {
            this.CorpId = CorpId;
            this.AppKey = AppKey;
            this.AppSecret = AppSecret;
            this.SuiteTicket = SuiteTicket;
            this.ApiServer = string.IsNullOrEmpty(ApiServer) ? CfgApiServer : ApiServer;
            handler = new Handler();
        }


        internal Task<ApiResult<TResponse>> CallAsync<TRequest, TResponse>(string operation, TRequest request, System.Net.Http.HttpMethod method = null)
            where TResponse : Wlniao.Handler.IResponse, new()
            where TRequest : Wlniao.Handler.IRequest
        {
            if (request == null)
            {
                throw new ArgumentNullException();
            }

            var ctx = new Context();
            ctx.CorpId = CorpId;
            ctx.AppKey = AppKey;
            ctx.AppSecret = AppSecret;
            ctx.Method = method == null ? System.Net.Http.HttpMethod.Get : method;
            ctx.Operation = operation;
            ctx.Request = request;
            ctx.RequestHost = ApiServer;

            handler.HandleBefore(ctx);
            if (ctx.Response == null)
            {
                return ctx.HttpTask.ContinueWith((t) =>
                {
                    handler.HandleAfter(ctx);
                    if (ctx.Response is Error)
                    {
                        var err = (Error)ctx.Response;
                        return new ApiResult<TResponse>() { success = false, message = err.errmsg, code = err.errcode.ToString() };
                    }
                    return new ApiResult<TResponse>() { success = true, message = "success", data = (TResponse)ctx.Response };
                });
            }
            else
            {
                if (ctx.Response is Error)
                {
                    var err = (Error)ctx.Response;
                    return Task<ApiResult<TResponse>>.Run(() =>
                    {
                        return new ApiResult<TResponse>() { success = false, message = err.errmsg, code = err.errcode.ToString() };
                    });
                }
                else
                {
                    return Task<ApiResult<TResponse>>.Run(() =>
                    {
                        return new ApiResult<TResponse>() { success = true, message = "error", data = (TResponse)ctx.Response };
                    });
                }
            }
        }
        internal TResponse GetResponseFromAsyncTask<TResponse>(Task<TResponse> task)
        {
            try
            {
                task.Wait();
            }
            catch (System.AggregateException e)
            {
                throw e.GetBaseException();
            }

            return task.Result;
        }


        private class TokenCache
        {
            public int code = 0;
            public String info = "";
            public String token = "";
            public DateTime past = DateTime.MinValue;
        }
        private class TicketCache
        {
            public int code = 0;
            public String info = "";
            public String ticket = "";
            public DateTime past = DateTime.MinValue;
        }
        private static Dictionary<String, TokenCache> tokens = new Dictionary<string, TokenCache>();
        private static Dictionary<String, TicketCache> tickets = new Dictionary<string, TicketCache>();

        /// <summary>
        /// 获取access_token
        /// </summary>
        public void SetToken(String token)
        {
            var key = "key" + AppKey + CorpId;
            if (!tokens.ContainsKey(key))
            {
                tokens.Add(key, new TokenCache());
            }
            tokens[key].token = token;
            tokens[key].past = DateTime.Now.AddSeconds(3600);
        }
        /// <summary>
        /// 
        /// </summary>
        public int StatusCode()
        {
            var key = "key" + AppKey + CorpId;
            if (!tokens.ContainsKey(key))
            {
                tokens.Add(key, new TokenCache());
            }
            return tokens[key].code;
        }
        /// <summary>
        /// 
        /// </summary>
        public string StatusInfo()
        {
            var key = AppKey + CorpId;
            if (!tokens.ContainsKey(key))
            {
                tokens.Add(key, new TokenCache());
            }
            return tokens[key].info;
        }

        #region GetToken 获取access_token
        /// <summary>
        /// 获取access_token
        /// </summary>
        public string GetToken(Boolean renew = false)
        {
            var key = "key" + AppKey + CorpId;
            try
            {
                if (!tokens.ContainsKey(key))
                {
                    tokens.Add(key, new TokenCache());
                }
            }
            catch { }
            if (renew || tokens[key].past < DateTime.Now)
            {
                var rlt = GetResponseFromAsyncTask(CallAsync<GetTokenRequest, GetTokenResponse>("gettoken", new GetTokenRequest()
                {
                    appkey = AppKey,
                    appsecret = AppSecret
                }, System.Net.Http.HttpMethod.Get));
                if (rlt.success && rlt.data != null && rlt.data.errcode == 0 && !string.IsNullOrEmpty(rlt.data.access_token))
                {
                    tokens[key].code = 0;
                    tokens[key].info = "";
                    tokens[key].token = rlt.data.access_token;
                    tokens[key].past = DateTime.Now.AddSeconds(3600);
                }
                else
                {
                    tokens[key].code = rlt.data.errcode;
                    tokens[key].info = rlt.data.errmsg;
                }
#if DEBUG
                Console.WriteLine("Dingtalk.GetToken:" + tokens[key].token);
#endif
            }
            return tokens[key].token;
        }
        #endregion 

        #region GetCorpToken 获取access_token
        /// <summary>
        /// 获取access_token
        /// </summary>
        public string GetCorpToken(Boolean renew = false)
        {
            var key = "key" + AppKey + CorpId;
            if (!tokens.ContainsKey(key))
            {
                tokens.Add(key, new TokenCache());
            }
            if (renew || tokens[key].past < DateTime.Now)
            {
                var rlt = GetResponseFromAsyncTask(CallAsync<GetCorpTokenRequest, GetCorpTokenResponse>("getcorptoken", new GetCorpTokenRequest()
                {
                    accessKey = AppKey,
                    auth_corpid = CorpId,
                    suiteTicket = SuiteTicket
                }, System.Net.Http.HttpMethod.Get));
                if (rlt.success && rlt.data != null && rlt.data.errcode == 0 && !string.IsNullOrEmpty(rlt.data.access_token))
                {
                    tokens[key].code = 0;
                    tokens[key].info = "";
                    tokens[key].token = rlt.data.access_token;
                    tokens[key].past = DateTime.Now.AddSeconds(3600);
                }
                else
                {
                    tokens[key].code = rlt.data.errcode;
                    tokens[key].info = rlt.data.errmsg;
                }
            }
            return tokens[key].token;
        }
        #endregion

        #region GetTicket 获取jsapi_ticket
        /// <summary>
        /// 获取jsapi_ticket
        /// </summary>
        public string GetTicket(Boolean renew = false)
        {
            var key = "key" + AppKey + CorpId;
            try
            {
                if (!tickets.ContainsKey(key))
                {
                    tickets.Add(key, new TicketCache());
                }
            }
            catch { }
            if (renew || tickets[key].past < DateTime.Now)
            {
                var rlt = GetResponseFromAsyncTask(CallAsync<GetTicketRequest, GetTicketResponse>("getticket", new GetTicketRequest()
                {
                    access_token = string.IsNullOrEmpty(this.SuiteTicket) ? GetToken() : GetCorpToken()
                }, System.Net.Http.HttpMethod.Get));
                if (rlt.success && rlt.data != null && rlt.data.errcode == 0 && !string.IsNullOrEmpty(rlt.data.ticket))
                {
                    tickets[key].code = 0;
                    tickets[key].info = "";
                    tickets[key].ticket = rlt.data.ticket;
                    tickets[key].past = DateTime.Now.AddSeconds(3600);
                }
                else
                {
                    tickets[key].code = rlt.data.errcode;
                    tickets[key].info = rlt.data.errmsg;
                }
            }
            return tickets[key].ticket;
        }
        #endregion 

        #region GetAuthUserByCode 获取免登授权码用户详情
        /// <summary>
        /// 获取免登授权码用户详情
        /// </summary>
        public ApiResult<Models.AuthUser> GetAuthUserByCode(String code)
        {
            var res = GetResponseFromAsyncTask(CallAsync<GetAuthUserByCodeRequest, GetAuthUserByCodeResponse>("getuserauth_bycode", new GetAuthUserByCodeRequest()
            {
                code = code,
                access_token = string.IsNullOrEmpty(this.SuiteTicket) ? GetToken() : GetCorpToken()
            }));
            var rlt = new ApiResult<Models.AuthUser> { message = res.message };
            if (res.success && res.data != null)
            {
                if (res.data.errcode != 0)
                {
                    rlt.code = res.data.errcode.ToString();
                    rlt.message = res.data.errmsg;
                }
                else if (res.data != null)
                {
                    rlt.data = new Models.AuthUser
                    {
                        userid = res.data.userid,
                        deviceId = res.data.deviceId,
                        is_sys = res.data.is_sys,
                        sys_level = res.data.sys_level
                    };
                    rlt.success = true;
                }
            }
            return rlt;
        }
        #endregion 

        #region GetAuthinfoByCode 通过临时授权码获取授权用户的个人信息
        /// <summary>
        /// 通过临时授权码获取授权用户的个人信息
        /// </summary>
        public ApiResult<Models.AuthInfo> GetAuthinfoByCode(String code, String appid, String appsecret)
        {
            var res = GetResponseFromAsyncTask(CallAsync<GetAuthinfoByCodeRequest, GetAuthinfoByCodeResponse>("getuserinfo_bycode", new GetAuthinfoByCodeRequest()
            {
                accessKey = appid,
                appSecret = appsecret,
                tmp_auth_code = code
            }, System.Net.Http.HttpMethod.Post));
            var rlt = new ApiResult<Models.AuthInfo> { message = res.message };
            if (res.success && res.data != null)
            {
                if (res.data.errcode != 0)
                {
                    rlt.code = res.data.errcode.ToString();
                    rlt.message = res.data.errmsg;
                }
                else if (res.data != null)
                {
                    rlt.data = res.data.user_info;
                    rlt.success = true;
                }
            }
            return rlt;
        }
        #endregion

        #region GetUser 获取用户详情
        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public ApiResult<Models.User> GetUser(String userid, String lang = "")
        {
            var res = GetResponseFromAsyncTask(CallAsync<GetUserRequest, GetUserResponse>("get_user", new GetUserRequest()
            {
                userid = userid,
                lang = lang,
                access_token = string.IsNullOrEmpty(this.SuiteTicket) ? GetToken() : GetCorpToken(),
            }));
            var rlt = new ApiResult<Models.User> { message = res.message };
            if (res.success && res.data != null)
            {
                if (res.data.errcode != 0)
                {
                    rlt.code = res.data.errcode.ToString();
                    rlt.message = res.data.errmsg;
                }
                else if (res.data != null)
                {
                    rlt.data = new Models.User
                    {
                        userid = res.data.userid,
                        unionid = res.data.unionid,
                        name = res.data.name,
                        mobile = res.data.mobile,
                        avatar = res.data.avatar,
                        position = res.data.position,
                        jobnumber = res.data.jobnumber,
                        department = res.data.department,
                        remark = res.data.remark,
                        active = res.data.active,
                    };
                    rlt.success = true;
                }
            }
            return rlt;
        }
        #endregion

        //#region GetUserIdByMobile 根据手机号获取员工userid
        ///// <根据手机号获取员工userid>
        ///// 获取用户详情
        ///// </summary>
        ///// <param name="mobile">员工手机号</param>
        ///// <returns></returns>
        //public ApiResult<String> GetUserIdByMobile(String mobile)
        //{
        //    var res = GetResponseFromAsyncTask(CallAsync<GetUserIdByMobileRequest, GetUserIdByMobileResponse>("get_by_mobile", new GetUserIdByMobileRequest()
        //    {
        //        mobile = mobile,
        //        access_token = string.IsNullOrEmpty(this.SuiteTicket) ? GetToken() : GetCorpToken(),
        //    }));
        //    var rlt = new ApiResult<String> { message = res.message };
        //    if (res.success && res.data != null)
        //    {
        //        if (res.data.errcode != 0)
        //        {
        //            rlt.code = res.data.errcode.ToString();
        //            rlt.message = res.data.errmsg;
        //        }
        //        else if (res.data != null && !string.IsNullOrEmpty(res.data.userid))
        //        {
        //            rlt.data = res.data.userid;
        //            rlt.success = true;
        //        }
        //    }
        //    return rlt;
        //}
        //#endregion 


        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="textParams"></param>
        /// <param name="fileParams"></param>
        /// <returns></returns>
        public static byte[] DoPost(IDictionary<string, string> textParams, IDictionary<string, Models.FileItem> fileParams,out String contentType)
        {
            if (fileParams == null || fileParams.Count == 0)
            {
                contentType = "application/x-www-form-urlencoded;charset=utf-8";
                var query = new System.Text.StringBuilder();
                var hasParam = false;
                foreach (KeyValuePair<string, string> kv in textParams)
                {
                    string name = kv.Key;
                    string value = kv.Value;
                    // 忽略参数名或参数值为空的参数
                    if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
                    {
                        if (hasParam)
                        {
                            query.Append("&");
                        }

                        query.Append(name);
                        query.Append("=");
                        query.Append(System.Web.HttpUtility.UrlEncode(value, Encoding.UTF8));
                        hasParam = true;
                    }
                }

                return Encoding.UTF8.GetBytes(query.ToString());
            }
            else
            {
                var total = 0;
                var maxlength = ((textParams == null || textParams.Count == 0) ? 0 : textParams.Values.Sum(a => a.Length))
                    + (fileParams.Values.Sum(a => a.Bytes.Length) + textParams.Count * 50) + 1000;
                var revBuffer = new byte[maxlength];
                var reqStream = new System.IO.MemoryStream();
                var boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线
                contentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;

                byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
                byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

                if (textParams != null && textParams.Count > 0)
                {
                    // 组装文本请求参数
                    string textTemplate = "Content-Disposition:form-data;name=\"{0}\"\r\nContent-Type:text/plain\r\n\r\n{1}";
                    foreach (KeyValuePair<string, string> kv in textParams)
                    {
                        var itemBytes = Encoding.UTF8.GetBytes(string.Format(textTemplate, kv.Key, kv.Value));
                        Buffer.BlockCopy(itemBoundaryBytes, 0, revBuffer, total, itemBoundaryBytes.Length);
                        total += itemBoundaryBytes.Length;
                        Buffer.BlockCopy(itemBytes, 0, revBuffer, total, itemBytes.Length);
                        total += itemBytes.Length;
                    }
                }

                // 组装文件请求参数
                string fileTemplate = "Content-Disposition:form-data;name=\"{0}\";filename=\"{1}\"\r\nContent-Type:{2}\r\n\r\n";
                foreach (var kv in fileParams)
                {
                    var itemBytes = Encoding.UTF8.GetBytes(string.Format(fileTemplate, kv.Key, kv.Value.FileName, kv.Value.MimeType));

                    Buffer.BlockCopy(itemBoundaryBytes, 0, revBuffer, total, itemBoundaryBytes.Length);
                    total += itemBoundaryBytes.Length;
                    Buffer.BlockCopy(itemBytes, 0, revBuffer, total, itemBytes.Length);
                    total += itemBytes.Length;
                    Buffer.BlockCopy(kv.Value.Bytes, 0, revBuffer, total, kv.Value.Bytes.Length);
                    total += kv.Value.Bytes.Length;
                }
                Buffer.BlockCopy(endBoundaryBytes, 0, revBuffer, total, endBoundaryBytes.Length);
                total += endBoundaryBytes.Length;
                return revBuffer.Take(total).ToArray();
            }
        }


    }
}