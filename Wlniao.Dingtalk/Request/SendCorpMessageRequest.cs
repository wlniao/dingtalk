using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Request
{
    /// <summary>
    /// 发送工作通知消息 的请求参数
    /// </summary>
    public class SendCorpMessageRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 应用agentId
        /// </summary>
        public string agent_id { get; set; }
        /// <summary>
        /// 接收者的用户userid列表，最大列表长度：100
        /// </summary>
        public string userid_list { get; set; }
        /// <summary>
        /// 接收者的部门id列表，最大列表长度：20,  接收者是部门id下(包括子部门下)的所有用户
        /// </summary>
        public string dept_id_list { get; set; }
        /// <summary>
        /// 是否发送给企业全部用户
        /// </summary>
        public bool to_all_user { get; set; }
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