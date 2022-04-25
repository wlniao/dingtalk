using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.User
{
    /// <summary>
    /// 根据手机号查询用户 的输出内容
    /// </summary>
    public class GetByMobileResponse : BaseResponse
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
            /// 员工在当前企业内的唯一标识，也称staffId。可由企业在创建时指定，并代表一定含义比如工号，创建后不可修改
            /// </summary>
            public string userid { get; set; }
            /// <summary>
            /// 专属帐号员工的userid列表(不含其他组织创建的专属帐号)
            /// </summary>
            public List<string> exclusive_account_userid_list { get; set; }
        }
    }
}
