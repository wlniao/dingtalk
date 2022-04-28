using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Todo
{
    /// <summary>
    /// 待办通知配置
    /// </summary>
    public class NotifyConfigs
    {
        /// <summary>
        /// DING通知配置，目前仅支持取值为1，表示应用内DING
        /// </summary>
        public string dingNotify { get; set; }
    }
}