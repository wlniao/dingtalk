using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Auth
{
    /// <summary>
    /// 获取access_token 的请求参数
    /// </summary>
    public class AccessToken : Context
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public AccessToken(AccessTokenRequest obj)
        {
            base.Method = "POST";
            base.OldHost = false;
            base.ApiPath = "/v1.0/oauth2/accessToken";
            base.RequestBody = obj;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="rlt"></param>
        public override void CheckRespose<TResponse>(ApiResult<TResponse> rlt)
        {
            var res = rlt.data as AccessTokenResponse;
            if (res == null)
            {
                rlt.message = "内容输出无效";
            }
            else if(!string.IsNullOrEmpty(res.accessToken))
            {
                rlt.success = true;
            }
        }
    }
}