using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Auth
{
    /// <summary>
    /// 获取第三方应用授权企业的accessToken 的请求参数
    /// </summary>
    public class CorpAccessToken : Context
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public CorpAccessToken(CorpAccessTokenRequest obj)
        {
            base.Method = "POST";
            base.ApiHost = "https://api.dingtalk.com";
            base.ApiPath = "/v1.0/oauth2/corpAccessToken";
            base.RequestBody = obj;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="rlt"></param>
        public override void CheckRespose<TResponse>(ApiResult<TResponse> rlt)
        {
            var res = rlt.data as CorpAccessTokenResponse;
            if (res == null)
            {
                rlt.message = "内容输出无效";
            }
            else if (!string.IsNullOrEmpty(res.accessToken))
            {
                rlt.success = true;
            }
        }
    }
}