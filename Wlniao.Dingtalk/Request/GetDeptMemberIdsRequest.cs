using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Request
{
    /// <summary>
    /// 获取部门用户userid列表 的请求参数
    /// </summary>
    public class GetDeptMemberIdsRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 获取的部门id
        /// </summary>
        public string deptId { get; set; }
        /// <summary>
        /// 调用接口凭证
        /// </summary>
        public string access_token { get; set; }
    }
}