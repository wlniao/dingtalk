using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Department
{
    /// <summary>
    /// 获取部门列表 的输出内容
    /// </summary>
    public class ListSubDepartmentResponse : BaseResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public List<DeptBaseResponse> result { get; set; }

        /// <summary>
        /// 返回结果。
        /// </summary>
        public class DeptBaseResponse
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
            /// 是否同步创建一个关联此部门的企业群
            /// </summary>
            public bool create_dept_group { get; set; }
            /// <summary>
            /// 当部门群已经创建后，是否有新人加入部门会自动加入该群
            /// </summary>
            public bool auto_add_user { get; set; }
        }
    }
}
