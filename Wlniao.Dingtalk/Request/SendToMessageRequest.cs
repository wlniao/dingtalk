using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Request
{
    /// <summary>
    /// 发送普通消息 的请求参数
    /// </summary>
    public class SendToMessageRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 消息发送者 userId
        /// </summary>
        public string sender { get; set; }
        /// <summary>
        /// 群会话或者个人会话的id
        /// </summary>
        public string cid { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public Object msg { get; set; }
        /// <summary>
        /// 调用接口凭证
        /// </summary>
        public string access_token { get; set; }
    }
}