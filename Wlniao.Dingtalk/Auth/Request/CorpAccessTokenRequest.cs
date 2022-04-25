using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Auth
{
    /// <summary>
    /// 获取第三方应用授权企业的accessToken 的请求参数
    /// </summary>
    public class CorpAccessTokenRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 已创建的第三方企业应用的SuiteKey。
        /// </summary>
        public string suiteKey { get; set; }
        /// <summary>
        /// 已创建的第三方企业应用的SuiteSecret。
        /// </summary>
        public string suiteSecret { get; set; }
        /// <summary>
        /// 授权企业的CorpId
        /// </summary>
        public string authCorpId { get; set; }
        /// <summary>
        /// 钉钉推送的suiteTicket（代开发应用无需提供）
        /// </summary>
        public string suiteTicket { get; set; }
    }
}