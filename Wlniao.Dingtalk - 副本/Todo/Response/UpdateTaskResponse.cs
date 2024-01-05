using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Todo
{
    /// <summary>
    /// 更新钉钉待办任务 的输出内容
    /// </summary>
    public class UpdateTaskResponse : BaseResponse
    {
        /// <summary>
        /// 更新结果
        /// </summary>
        public bool result { get; set; }
    }
}
