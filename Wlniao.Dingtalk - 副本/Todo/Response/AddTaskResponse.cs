using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Todo
{
    /// <summary>
    /// 新增钉钉待办任务 的输出内容
    /// </summary>
    public class AddTaskResponse : BaseResponse
    {
        /// <summary>
        /// 待办ID
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 待办的标题
        /// </summary>
        public string subject { get; set; }
        /// <summary>
        /// 待办描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// Unix时间戳，单位毫秒
        /// </summary>
        public long startTime { get; set; }
        /// <summary>
        /// 截止时间，Unix时间戳，单位毫秒
        /// </summary>
        public long dueTime { get; set; }
        /// <summary>
        /// 完成时间，Unix时间戳，单位毫秒
        /// </summary>
        public long finishTime { get; set; }
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
        /// <summary>
        /// 详情页url跳转地址
        /// </summary>
        public DetailUrl detailUrl { get; set; }
        /// <summary>
        /// 业务来源
        /// </summary>
        public string source { get; set; }
        /// <summary>
        /// 业务系统侧的唯一标识ID，即业务ID
        /// </summary>
        public string sourceId { get; set; }
        /// <summary>
        /// 创建时间，Unix时间戳，单位毫秒
        /// </summary>
        public long createdTime { get; set; }
        /// <summary>
        /// 更新时间，Unix时间戳，单位毫秒
        /// </summary>
        public long modifiedTime { get; set; }
        /// <summary>
        /// 创建者的unionId
        /// </summary>
        public string creatorId { get; set; }
        /// <summary>
        /// 更新者的unionId
        /// </summary>
        public string modifierId { get; set; }
        /// <summary>
        /// 接入应用标识
        /// </summary>
        public string bizTag { get; set; }
        /// <summary>
        /// 请求ID
        /// </summary>
        public string requestId { get; set; }
        /// <summary>
        /// 生成的待办是否仅展示在执行者的待办列表中
        /// </summary>
        public bool isOnlyShowExecutor { get; set; }
        /// <summary>
        /// 优先级，取值：10：较低 20：普通 30：紧急 40：非常紧急
        /// </summary>
        public int priority { get; set; }
        /// <summary>
        /// 待办通知配置
        /// </summary>
        public NotifyConfigs notifyConfigs { get; set; }

    }
}
