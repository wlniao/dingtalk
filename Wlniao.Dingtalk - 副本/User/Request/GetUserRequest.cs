using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.User
{
    /// <summary>
    /// 查询用户详情 的请求参数
    /// </summary>
    public class GetUserRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 用户的userId
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 通讯录语言 (默认值：zh_CN)
        /// </summary>
        public string language { get; set; }
    }
}