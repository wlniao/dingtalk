using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Request
{
    /// <summary>
    /// 获取jsapi_ticket 的请求参数
    /// </summary>
    public class GetTicketRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 调用接口凭证
        /// </summary>
        public string access_token { get; set; }
    }
}