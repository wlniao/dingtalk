using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Request
{
    /// <summary>
    /// 发送钉盘文件给指定用户 的请求参数
    /// </summary>
    public class AddToSingleChatRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 文件发送者微应用的agentIdd
        /// </summary>
        public string agent_id { get; set; }
        /// <summary>
        /// 文件接收人的userid
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 调用钉盘上传文件接口得到的mediaid，需要utf-8 urlEncode
        /// </summary>
        public string media_id { get; set; }
        /// <summary>
        /// 文件名(需包含扩展名)，需要utf-8 urlEncode
        /// </summary>
        public string file_name { get; set; }
        /// <summary>
        /// 调用接口凭证
        /// </summary>
        public string access_token { get; set; }
    }
}