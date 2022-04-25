using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Request
{
    /// <summary>
    /// 发起审批实例 的请求参数
    /// </summary>
    public class ProcessInstanceCreateRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 企业应用标识(ISV调用必须设置)
        /// </summary>
        public string agent_id { get; set; }
        /// <summary>
        /// 审批流的唯一码，process_code就在审批流编辑的页面URL中
        /// </summary>
        public string process_code { get; set; }
        /// <summary>
        /// 审批实例发起人的userid
        /// </summary>
        public string originator_user_id { get; set; }
        /// <summary>
        /// 发起人所在的部门，如果发起人属于根部门，传-1
        /// </summary>
        public string dept_id { get; set; }
        /// <summary>
        /// 审批人userid列表，最大列表长度：20。多个审批人用逗号分隔，按传入的顺序依次审批
        /// </summary>
        public string approvers { get; set; }
        /// <summary>
        /// 审批人列表，支持会签/或签，优先级高于approvers变量
        /// </summary>
        public List<Models.ProcessInstanceApproverVo> approvers_v2 { get; set; }
        /// <summary>
        /// 抄送人userid列表，最大列表长度：20。多个抄送人用逗号分隔。该参数需要与cc_position参数一起传，抄送人才能生效。
        /// </summary>
        public string cc_list { get; set; }
        /// <summary>
        /// 抄送时间，分为（START, FINISH, START_FINISH）。
        /// </summary>
        public string cc_position { get; set; }
        /// <summary>
        /// 审批流表单参数，最大列表长度：20。
        /// </summary>
        public List<Models.FormComponentVo> form_component_values { get; set; }
        /// <summary>
        /// 调用接口凭证
        /// </summary>
        public string access_token { get; set; }
    }
}