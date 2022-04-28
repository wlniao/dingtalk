using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Todo
{
    /// <summary>
    /// 删除钉钉待办任务 的输出内容
    /// </summary>
    public class DeleteTaskResponse : BaseResponse
    {
        /// <summary>
        /// 更新结果
        /// </summary>
        public bool result { get; set; }
        /// <summary>
        /// 请求ID
        /// </summary>
        public string requestId { get; set; }
    }
}
