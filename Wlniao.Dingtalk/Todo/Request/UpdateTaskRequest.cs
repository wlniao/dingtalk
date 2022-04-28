using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Todo
{
    /// <summary>
    /// 更新钉钉待办任务 的请求参数
    /// </summary>
    public class UpdateTaskRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 待办的标题
        /// </summary>
        public string subject { get; set; }
        /// <summary>
        /// 待办描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 截止时间，Unix时间戳，单位毫秒
        /// </summary>
        public long dueTime { get; set; }
        /// <summary>
        /// 完成状态
        /// </summary>
        public bool done { get; set; }
        /// <summary>
        /// 执行者的unionId
        /// </summary>
        public string[] executorIds { get; set; }
        /// <summary>
        /// 参与者的unionId
        /// </summary>
        public string[] participantIds { get; set; }
    }
}