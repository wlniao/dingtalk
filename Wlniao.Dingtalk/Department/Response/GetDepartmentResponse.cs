using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Department
{
    /// <summary>
    /// 查询用户详情 的输出内容
    /// </summary>
    public class GetDepartmentResponse : BaseResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public DeptGetResponse result { get; set; }

        /// <summary>
        /// 返回结果。
        /// </summary>
        public class DeptGetResponse
        {
            /// <summary>
            /// 部门ID
            /// </summary>
            public string dept_id { get; set; }
            /// <summary>
            /// 部门ID。
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 父部门ID
            /// </summary>
            public string parent_id { get; set; }
            /// <summary>
            /// 部门标识字段。第三方企业应用不返回该参数。
            /// </summary>
            public string source_identifier { get; set; }
            /// <summary>
            /// 是否同步创建一个关联此部门的企业群
            /// </summary>
            public bool create_dept_group { get; set; }
            /// <summary>
            /// 当部门群已经创建后，是否有新人加入部门会自动加入该群
            /// </summary>
            public bool auto_add_user { get; set; }
            /// <summary>
            /// 是否默认同意加入该部门的申请
            /// </summary>
            public bool auto_approve_apply { get; set; }
            /// <summary>
            /// 部门是否来自关联组织
            /// </summary>
            public bool from_union_org { get; set; }
            /// <summary>
            /// 教育部门标签
            /// </summary>
            public string tags { get; set; }
            /// <summary>
            /// 在父部门中的次序值
            /// </summary>
            public string order { get; set; }
            /// <summary>
            /// 部门群ID
            /// </summary>
            public string dept_group_chat_id { get; set; }
            /// <summary>
            /// 部门群是否包含子部门
            /// </summary>
            public bool group_contain_sub_dept { get; set; }
            /// <summary>
            /// 企业群群主userId
            /// </summary>
            public string org_dept_owner { get; set; }
            /// <summary>
            /// 部门的主管userd列表
            /// </summary>
            public List<string> dept_manager_userid_list { get; set; }
            /// <summary>
            /// 是否限制本部门成员查看通讯录
            /// </summary>
            public bool outer_dept { get; set; }
            /// <summary>
            /// 配置的部门员工可见部门Id列表
            /// </summary>
            public List<int> outer_permit_depts { get; set; }
            /// <summary>
            /// 配置的部门员工可见员工userId列表
            /// </summary>
            public List<string> outer_permit_users { get; set; }
            /// <summary>
            /// 是否开启隐藏本部门
            /// </summary>
            public bool hide_dept { get; set; }
            /// <summary>
            /// 隐藏部门的员工userId列表
            /// </summary>
            public List<string> user_permits { get; set; }
            /// <summary>
            /// 隐藏部门的部门Id列表
            /// </summary>
            public List<int> dept_permits { get; set; }
        }
    }
}
