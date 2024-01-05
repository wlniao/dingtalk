using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.User
{
    /// <summary>
    /// 通过免登码获取用户信息 的请求参数
    /// </summary>
    public class GetUserInfoRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 免登授权码
        /// </summary>
        public string code { get; set; }
    }
}