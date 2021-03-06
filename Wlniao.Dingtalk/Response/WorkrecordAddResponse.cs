﻿using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Response
{
    /// <summary>
    /// 发起待办 的输出内容
    /// </summary>
    public class WorkRecordAddResponse : Wlniao.Handler.IResponse
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
        /// 待办事项唯一id，更新待办事项的时候需要用到
        /// </summary>
        public string record_id { get; set; }
    }
}
