using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wlniao.Dingtalk.Auth;
namespace Wlniao.Dingtalk
{
    /// <summary>
    /// 钉钉应用客户端
    /// </summary>
    public class Client : Wlniao.Handler.IClient
    {
        #region 企业内部开发配置信息
        internal static string _OldHost = null;     //API接口服务器
        internal static string _ApiHost = null;     //API接口服务器
        internal static string _CorpId = null;      //组织机构Id
        internal static string _AgentId = null;     //应用安装Id
        internal static string _Token = null;       //接口通讯凭据
        internal static string _AesKey = null;      //接口通讯密钥
        internal static string _AppKey = null;      //应用开发标识
        internal static string _AppSecret = null;   //应用开发密钥
        /// <summary>
        /// 开放平台接口前缀
        /// </summary>
        public static string CfgApiHostOld
        {
            get
            {
                if (_OldHost == null)
                {
                    _OldHost = Config.GetSetting("DingHostOld", "https://oapi.dingtalk.com");
                }
                return _OldHost;
            }
        }
        /// <summary>
        /// 开放平台接口前缀
        /// </summary>
        public static string CfgApiHost
        {
            get
            {
                if (_ApiHost == null)
                {
                    _ApiHost = Config.GetSetting("DingHost", "https://api.dingtalk.com");
                }
                return _ApiHost;
            }
        }
        /// <summary>
        /// 组织机构Id
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
        /// 应用安装Id
        /// </summary>
        public static string CfgAgentId
        {
            get
            {
                if (_AgentId == null)
                {
                    _AgentId = Config.GetSetting("DingAgentId");
                }
                return _AgentId;
            }
        }
        /// <summary>
        /// 接口通讯凭据
        /// </summary>
        public static string CfgToken
        {
            get
            {
                if (_Token == null)
                {
                    _Token = Config.GetSetting("DingToken");
                }
                return _Token;
            }
        }
        /// <summary>
        /// 接口通讯密钥
        /// </summary>
        public static string CfgAesKey
        {
            get
            {
                if (_AesKey == null)
                {
                    _AesKey = Config.GetSetting("DingAesKey");
                }
                return _AesKey;
            }
        }
        /// <summary>
        /// 应用开发标识
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
        /// 应用开发密钥
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
        #endregion

        /// <summary>
        /// 组织机构Id
        /// </summary>
        public string CorpId { get; set; }
        /// <summary>
        /// 应用安装Id
        /// </summary>
        public string AgentId { get; set; }
        /// <summary>
        /// 接口通讯凭据
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 接口通讯密钥
        /// </summary>
        public string AesKey { get; set; }
        /// <summary>
        /// 应用开发标识
        /// </summary>
        public string AppKey { get; set; }
        /// <summary>
        /// 应用开发密钥
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        /// 接口调用凭据
        /// </summary>
        public string AppTicket { get; set; }
        /// <summary>
        /// 接口调用凭据
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// 获取access_token
        /// </summary>
        /// <returns></returns>
        public Boolean GetAccessToken
        {
            get
            {
                if (string.IsNullOrEmpty(AccessToken))
                {
                    if (string.IsNullOrEmpty(AppKey))
                    {
                        log.Topic("dingtalk", "\r\nAppKey not set, must add DingAppKey environment variable");
                    }
                    else if (string.IsNullOrEmpty(AppSecret))
                    {
                        log.Topic("dingtalk", "\r\nAppSecret not set, must add DingAppSecret environment variable");
                    }
                    else
                    {
                        var rlt = Handle<AccessTokenResponse>(new AccessToken(new AccessTokenRequest
                        {
                            appKey = AppKey,
                            appSecret = AppSecret
                        }));
                        if (rlt.success)
                        {
                            AccessToken = rlt.data.access_token;
                        }
                    }
                }
                return string.IsNullOrEmpty(AccessToken) ? false : true;
            }
        }
        /// <summary>
        /// 获取第三方应用授权企业的accessToken
        /// </summary>
        /// <returns></returns>
        public Boolean GetCorpAccessToken
        {
            get
            {
                if (string.IsNullOrEmpty(AccessToken))
                {
                    if (string.IsNullOrEmpty(CorpId))
                    {
                        log.Error("Dingtalk error: CorpId not set, must add DingCorpId environment variable");
                    }
                    else if (string.IsNullOrEmpty(AppKey))
                    {
                        log.Error("Dingtalk error: AppKey not set, must add DingAppKey environment variable");
                    }
                    else if (string.IsNullOrEmpty(AppSecret))
                    {
                        log.Error("Dingtalk error: AppSecret not set, must add DingAppSecret environment variable");
                    }
                    else
                    {
                        var rlt = Handle<CorpAccessTokenResponse>(new CorpAccessToken(new CorpAccessTokenRequest
                        {
                            authCorpId = CorpId,
                            suiteKey = AppKey,
                            suiteSecret = AppSecret,
                            suiteTicket = AppTicket
                        }));
                        if (rlt.success)
                        {
                            AccessToken = rlt.data.accessToken;
                        }
                    }
                }
                return string.IsNullOrEmpty(AccessToken) ? false : true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Client()
        {
            this.CorpId = CfgCorpId;
            this.AgentId = CfgAgentId;
            this.AesKey = CfgAesKey;
            this.AppKey = CfgAppKey;
            this.AppSecret = CfgAppSecret;
        }
        /// <summary>
        /// 
        /// </summary>
        public Client(String accessToken)
        {
            this.CorpId = CfgCorpId;
            this.AgentId = CfgAgentId;
            this.AesKey = CfgAesKey;
            this.AppKey = CfgAppKey;
            this.AppSecret = CfgAppSecret;
            this.AccessToken = accessToken;
        }
        /// <summary>
        /// 
        /// </summary>
        public Client(String corpId, String agentId, String appSecret)
        {
            this.CorpId = corpId;
            this.AgentId = agentId;
            this.AppSecret = appSecret;
        }
        /// <summary>
        /// 
        /// </summary>
        public Client(String accessToken, String corpId, String agentId, String appSecret)
        {
            this.CorpId = corpId;
            this.AgentId = agentId;
            this.AppSecret = appSecret;
            this.AccessToken = accessToken;
        }

        /// <summary>
        /// 接口调用
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public ApiResult<TResponse> Handle<TResponse>(Context ctx)
            where TResponse : Wlniao.Handler.IResponse
        {
            if (string.IsNullOrEmpty(ctx.ApiHost))
            {
                ctx.ApiHost = ctx.OldHost ? CfgApiHostOld : CfgApiHost;
            }
            var task = HandleAsync<TResponse>(ctx);
            task.Wait();
            return task.Result;
        }
        /// <summary>
        /// 接口调用（异步）
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public Task<ApiResult<TResponse>> HandleAsync<TResponse>(Context ctx)
            where TResponse : Wlniao.Handler.IResponse
        {
            var result = new ApiResult<TResponse> { node = XCore.WebNode, code = "-1", success = false, message = "unkown error" };
            if (string.IsNullOrEmpty(ctx.ApiHost))
            {
                result.code = "400";
                result.message = "request host not set";
            }
            else if (string.IsNullOrEmpty(ctx.ApiPath))
            {
                result.code = "400";
                result.message = "request path not set";
            }
            else if (ctx.TokenRequired && string.IsNullOrEmpty(AccessToken))
            {
                result.code = "400";
                result.message = "client access_token not set";
            }
            else
            {
                System.Net.Http.HttpClient http = null;
                if (ctx.Certificate == null)
                {
                    http = new System.Net.Http.HttpClient();
                }
                else
                {
                    var handler = new System.Net.Http.HttpClientHandler();
                    handler.ClientCertificateOptions = System.Net.Http.ClientCertificateOption.Manual;
                    handler.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
                    handler.ClientCertificates.Add(ctx.Certificate);
                    http = new System.Net.Http.HttpClient(handler);
                }
                http.BaseAddress = new System.Uri(ctx.ApiHost);
                if (ctx.TokenRequired)
                {
                    ctx.ApiPath = ctx.ApiPath.Replace("ACCESS_TOKEN", AccessToken);
                }
                //Task<System.Net.Http.HttpResponseMessage> task = null;
                //if (ctx.Method == "GET")
                //{
                //    var query = ctx.RequestBody as string;
                //    if (!string.IsNullOrEmpty(query))
                //    {
                //        var link = ctx.ApiPath.IndexOf('?') < 0 ? '?' : '&';
                //        ctx.ApiPath += query[0] == '?' || query[0] == '&' ? query : link + query;
                //    }
                //    task = http.GetAsync(ctx.ApiPath);
                //}
                //else
                //{
                //    var text = ctx.RequestBody as string;
                //    var bytes = ctx.RequestBody as byte[];
                //    if (bytes != null)
                //    {
                //        task = http.PostAsync(ctx.ApiPath, new System.Net.Http.ByteArrayContent(bytes));
                //    }
                //    else if (!string.IsNullOrEmpty(text))
                //    {
                //        task = http.PostAsync(ctx.ApiPath, new System.Net.Http.StringContent(text, ctx.Encoding, ctx.ContentType));
                //    }
                //    else if (ctx.RequestBody != null)
                //    {
                //        task = http.PostAsync(ctx.ApiPath, new System.Net.Http.StringContent(Json.ToString(ctx.RequestBody), ctx.Encoding, ctx.ContentType));
                //    }
                //    else
                //    {
                //        task = http.PostAsync(ctx.ApiPath, new System.Net.Http.ByteArrayContent(new byte[0]));
                //    }
                //}
                //task.Result.Content.ReadAsStringAsync().ContinueWith((res) =>
                //{
                //    ctx.ResponseBody = res.Result;
                //    ctx.HttpResponseHeaders = new Dictionary<String, String>();
                //    try
                //    {
                //        result.code = "0";
                //        result.data = Newtonsoft.Json.JsonConvert.DeserializeObject<TResponse>(res.Result);
                //        result.message = task.Result.ReasonPhrase;
                //        foreach (var item in task.Result.Headers)
                //        {
                //            var em = item.Value.GetEnumerator();
                //            if (em.MoveNext())
                //            {
                //                ctx.HttpResponseHeaders.Add(item.Key.ToLower(), em.Current);
                //            }
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        result.code = "-1";
                //        result.message = ex.Message;
                //    }
                //}).Wait();

                try
                {
                    var res = ctx.Handle();
                    res.Wait();
                    result.code = "0";
                    result.data = Newtonsoft.Json.JsonConvert.DeserializeObject<TResponse>(res.Result);
                }
                catch (Exception ex)
                {
                    result.code = "-1";
                    result.message = ex.Message;
                }
                if (result.code == "0")
                {
                    var data = result.data as BaseResponse;
                    result.code = data?.errcode;
                    result.message = data?.errmsg;
                    ctx.CheckRespose(result);
                }
            }
            log.Info("Dingtalk\r\nrequest:\r\n" + (ctx.RequestBody as string) + "\r\nDingtalk response:\r\n" + (ctx.ResponseBody as string));
            return Task<ApiResult<TResponse>>.Run(() =>
            {
                return result;
            });
        }
    }
}