using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Request
{
    /// <summary>
    /// userid与openid互换 的请求参数
    /// </summary>
    public class ConvertToOpenidRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 企业内的成员id
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 调用接口凭证
        /// </summary>
        public string access_token { get; set; }
    }
}