using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.User
{
    /// <summary>
    /// 根据手机号查询用户 的请求参数
    /// </summary>
    public class GetByMobileRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 员工手机号码
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 是否支持通过手机号搜索专属帐号(不含其他组织创建的专属帐号)。
        /// </summary>
        public bool support_exclusive_account_search { get; set; }
    }
}