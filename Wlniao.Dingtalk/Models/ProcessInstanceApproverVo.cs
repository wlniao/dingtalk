using System;
namespace Wlniao.Dingtalk.Models
{
    /// <summary>
    /// 审批人信息
    /// </summary>
    public class ProcessInstanceApproverVo
    {
        /// <summary>
        /// 审批类型，会签：AND；或签：OR；单人：NONE
        /// </summary>
        public string task_action_type { get; set; }
        /// <summary>
        /// 审批人userid列表，会签/或签列表长度必须大于1，非会签/或签列表长度只能为1
        /// </summary>
        public System.Collections.Generic.List<String> user_ids { get; set; }
    }
}