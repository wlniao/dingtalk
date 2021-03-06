﻿using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Response
{
    /// <summary>
    /// 获取jsapi_ticket 的输出内容
    /// </summary>
    public class GetTicketResponse : Wlniao.Handler.IResponse
    {
        /// <summary>
        /// 出错返回码，为0表示成功，非0表示调用失败
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 返回码提示语
        /// </summary>
        public string errmsg { get; set; }
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
