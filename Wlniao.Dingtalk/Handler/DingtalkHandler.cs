using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Wlniao.Handler;
namespace Wlniao.Dingtalk
{
    /// <summary>
    /// 
    /// </summary>
    public class DingtalkHandler : PipelineHandler
    {
        internal Dictionary<string, ResponseEncoder> EncoderMap;
        internal Dictionary<string, ResponseDecoder> DecoderMap;
        internal delegate void ResponseEncoder(Context ctx);
        internal delegate void ResponseDecoder(Context ctx);

        /// <summary>
        /// 
        /// </summary>
        public DingtalkHandler(PipelineHandler handler) : base(handler)
        {
            EncoderMap = new Dictionary<string, ResponseEncoder>() {
                { "gettoken", GetTokenEncode },
                { "getcorptoken", GetCorpTokenEncode },
                { "getuserauth_bycode", GetAuthUserByCodeEncode },
                { "getuserinfo_bycode", GetAuthinfoByCodeEncode },
                { "get_user", GetUserEncode },
                { "get_by_mobile", GetUserEncode },
            };
            DecoderMap = new Dictionary<string, ResponseDecoder>() {
                { "gettoken", GetTokenDecode },
                { "getcorptoken", GetCorpTokenDecode },
                { "getuserauth_bycode", GetAuthUserByCodeDecode },
                { "getuserinfo_bycode", GetAuthinfoByCodeDecode },
                { "get_user", GetUserDecode },
                { "get_by_mobile", GetUserDecode },
            };
        }

        #region Handle
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        public override void HandleBefore(IContext ctx)
        {
            var _ctx = (Context)ctx;
            EncoderMap[_ctx.Operation](_ctx);
            if (string.IsNullOrEmpty(_ctx.RequestPath))
            {
                _ctx.RequestPath = _ctx.Operation;
            }
            inner.HandleBefore(ctx);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        public override void HandleAfter(IContext ctx)
        {
            inner.HandleAfter(ctx);
            var _ctx = (Context)ctx;
            DecoderMap[_ctx.Operation](_ctx);
        }
        #endregion

        #region GetToken
        private void GetTokenEncode(Context ctx)
        {
            var req = ctx.Request as Request.GetTokenRequest;
            if (req != null)
            {
                ctx.Method = System.Net.Http.HttpMethod.Get;
                ctx.RequestPath = "/gettoken"
                    + "?appkey=" + req.appkey + "&appsecret=" + req.appsecret;
            }
        }
        private void GetTokenDecode(Context ctx)
        {
            try
            {
                ctx.Response = JsonConvert.DeserializeObject<Response.GetTokenResponse>(ctx.HttpResponseString);
            }
            catch
            {
                ctx.Response = new Error() { errmsg = "InvalidJsonString" };
            }
        }
        #endregion

        #region GetCorpToken
        private void GetCorpTokenEncode(Context ctx)
        {
            var req = ctx.Request as Request.GetCorpTokenRequest;
            if (req != null)
            {
                var timestamp = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).Ticks / 10000).ToString();

                var hashAlgorithm = new System.Security.Cryptography.HMACSHA256(Encoding.UTF8.GetBytes(ctx.AppSecret));
                var bytes = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(timestamp + "\n" + req.suiteTicket));
                var signature = IO.Base64Encoder.Encoder.GetEncoded(bytes);

                ctx.Method = System.Net.Http.HttpMethod.Get;
                ctx.RequestPath = "/service/get_corp_token"
                    + "?signature=" + strUtil.UrlEncode(signature) + "&timestamp=" + timestamp + "&suiteTicket=" + req.suiteTicket + "&accessKey=" + req.accessKey;
            }
        }
        private void GetCorpTokenDecode(Context ctx)
        {
            try
            {
                ctx.Response = JsonConvert.DeserializeObject<Response.GetCorpTokenResponse>(ctx.HttpResponseString);
            }
            catch
            {
                ctx.Response = new Error() { errmsg = "InvalidJsonString" };
            }
        }
        #endregion

        #region GetAuthUserByCode
        private void GetAuthUserByCodeEncode(Context ctx)
        {
            var req = ctx.Request as Request.GetAuthUserByCodeRequest;
            if (req != null)
            {
                if (string.IsNullOrEmpty(req.code))
                {
                    ctx.Response = new Error() { errmsg = "missing code" };
                    return;
                }
                if (string.IsNullOrEmpty(req.access_token))
                {
                    ctx.Response = new Error() { errmsg = "missing access_token" };
                    return;
                }
                ctx.Method = System.Net.Http.HttpMethod.Get;
                ctx.RequestPath = "/user/getuserinfo"
                    + "?access_token=" + req.access_token
                    + "&code=" + req.code;
            }
        }
        private void GetAuthUserByCodeDecode(Context ctx)
        {
            try
            {
                ctx.Response = JsonConvert.DeserializeObject<Response.GetAuthUserByCodeResponse>(ctx.HttpResponseString);
            }
            catch
            {
                ctx.Response = new Error() { errmsg = "InvalidJsonString" };
            }
        }
        #endregion

        #region GetAuthinfoByCode
        private void GetAuthinfoByCodeEncode(Context ctx)
        {
            var req = ctx.Request as Request.GetAuthinfoByCodeRequest;
            if (req != null)
            {
                var timestamp = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).Ticks / 10000).ToString();

                var hashAlgorithm = new System.Security.Cryptography.HMACSHA256(Encoding.UTF8.GetBytes(req.appSecret));
                var bytes = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(timestamp));
                var signature = IO.Base64Encoder.Encoder.GetEncoded(bytes);

                ctx.Method = System.Net.Http.HttpMethod.Post;
                ctx.HttpRequestString = JsonConvert.SerializeObject(new { req.tmp_auth_code });
                ctx.RequestPath = "/sns/getuserinfo_bycode"
                    + "?signature=" + strUtil.UrlEncode(signature) + "&timestamp=" + timestamp + "&accessKey=" + req.accessKey;
            }
        }
        private void GetAuthinfoByCodeDecode(Context ctx)
        {
            try
            {
                ctx.Response = JsonConvert.DeserializeObject<Response.GetAuthinfoByCodeResponse>(ctx.HttpResponseString);
            }
            catch
            {
                ctx.Response = new Error() { errmsg = "InvalidJsonString" };
            }
        }
        #endregion

        #region GetUser
        private void GetUserEncode(Context ctx)
        {
            var req = ctx.Request as Request.GetUserRequest;
            if (req != null)
            {
                if (string.IsNullOrEmpty(req.userid))
                {
                    ctx.Response = new Error() { errmsg = "missing userid" };
                    return;
                }
                if (string.IsNullOrEmpty(req.access_token))
                {
                    ctx.Response = new Error() { errmsg = "missing access_token" };
                    return;
                }
                ctx.Method = System.Net.Http.HttpMethod.Get;
                ctx.RequestPath = "/user/get"
                    + "?access_token=" + req.access_token
                    + "&userid=" + req.userid
                    + "&lang=" + req.lang;
            }
        }
        private void GetUserDecode(Context ctx)
        {
            try
            {
                ctx.Response = JsonConvert.DeserializeObject<Response.GetUserResponse>(ctx.HttpResponseString);
            }
            catch
            {
                ctx.Response = new Error() { errmsg = "InvalidJsonString" };
            }
        }
        #endregion

        #region GetUserIdByMobile
        private void GetUserIdByMobileEncode(Context ctx)
        {
            var req = ctx.Request as Request.GetUserIdByMobileRequest;
            if (req != null)
            {
                if (string.IsNullOrEmpty(req.mobile))
                {
                    ctx.Response = new Error() { errmsg = "missing mobile" };
                    return;
                }
                if (string.IsNullOrEmpty(req.access_token))
                {
                    ctx.Response = new Error() { errmsg = "missing access_token" };
                    return;
                }
                ctx.Method = System.Net.Http.HttpMethod.Get;
                ctx.RequestPath = "/user/get_by_mobile"
                    + "?access_token=" + req.access_token
                    + "&mobile=" + req.mobile;
            }
        }
        private void GetUserIdByMobileDecode(Context ctx)
        {
            try
            {
                ctx.Response = JsonConvert.DeserializeObject<Response.GetUserIdByMobileResponse>(ctx.HttpResponseString);
            }
            catch
            {
                ctx.Response = new Error() { errmsg = "InvalidJsonString" };
            }
        }
        #endregion
    }
}