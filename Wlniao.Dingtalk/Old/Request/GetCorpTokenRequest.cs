using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Request
{
    /// <summary>
    /// 获取access_token 的请求参数
    /// </summary>
    public class GetCorpTokenRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 应用的唯一标识key
        /// </summary>
        public string accessKey { get; set; }
        /// <summary>
        /// 钉钉推送的suiteTicket。测试应用可以随意填写。
        /// </summary>
        public string suiteTicket { get; set; }
        /// <summary>
        /// 以timestamp+"\n"+suiteTicket为签名字符串，suiteSecret为签名密钥，使用算法HmacSHA256计算的签名值。注意：计算出签名以后，需要进行urlencode，才能把签名参数拼接到url中。
        /// </summary>
        public string signature { get; set; }
        /// <summary>
        /// 授权企业corpId,组装为JSON结构置于http post body部分
        /// </summary>
        public string auth_corpid { get; set; }
    }
}