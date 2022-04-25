using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Auth
{
    /// <summary>
    /// 获取jsapi_ticket 的输出内容
    /// </summary>
    public class GetTicketResponse : BaseResponse
    {
        /// <summary>
        /// 用于JSAPI的临时票据
        /// </summary>
        public string ticket { get; set; }
        /// <summary>
        /// 凭证的有效时间（秒）
        /// </summary>
        public int expires_in { get; set; }
    }
}
