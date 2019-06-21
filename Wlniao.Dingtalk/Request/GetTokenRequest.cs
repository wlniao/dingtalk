using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Request
{
    /// <summary>
    /// 获取access_token 的请求参数
    /// </summary>
    public class GetTokenRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 应用的唯一标识key
        /// </summary>
        public string appkey { get; set; }
        /// <summary>
        /// 应用的密钥
        /// </summary>
        public string appsecret { get; set; }
    }
}