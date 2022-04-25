using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.User
{
    /// <summary>
    /// 查询用户详情 的输出内容
    /// </summary>
    public class GetUserResponse : BaseResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public Result result { get; set; }

        /// <summary>
        /// 返回结果。
        /// </summary>
        public class Result
        {
            /// <summary>
            /// 员工在当前企业内的唯一标识。可由企业在创建时指定，并代表一定含义比如工号，创建后不可修改
            /// </summary>
            public string userid { get; set; }
            /// <summary>
            /// 员工在当前开发者企业账号范围内的唯一标识，系统生成，固定值，不会改变
            /// </summary>
            public string unionid { get; set; }
            /// <summary>
            /// 员工名字
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 手机号
            /// </summary>
            public string mobile { get; set; }
            /// <summary>
            /// 头像url
            /// </summary>
            public string avatar { get; set; }
            /// <summary>
            /// 职位信息
            /// </summary>
            public string title { get; set; }
            /// <summary>
            /// 员工工号
            /// </summary>
            public string job_number { get; set; }
            /// <summary>
            /// 员工的直属主管
            /// </summary>
            public string manager_userid { get; set; }
            /// <summary>
            /// 成员所属部门id列表
            /// </summary>
            public List<Int32> dept_id_list { get; set; }
            /// <summary>
            /// 备注
            /// </summary>
            public string remark { get; set; }
            /// <summary>
            /// 表示该用户是否激活了钉钉
            /// </summary>
            public bool active { get; set; }
        }
    }
}
