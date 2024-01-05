using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Auth
{
    /// <summary>
    /// 获取access_token 的请求参数
    /// </summary>
    public class AccessTokenRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 应用的唯一标识key
        /// </summary>
        public string appKey { get; set; }
        /// <summary>
        /// 应用的密钥
        /// </summary>
        public string appSecret { get; set; }
    }
}