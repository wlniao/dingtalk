using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Request
{
    /// <summary>
    /// 获取用户详情 的请求参数
    /// </summary>
    public class GetUserRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 员工id
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 通讯录语言(默认zh_CN，未来会支持en_US)
        /// </summary>
        public string lang { get; set; }
        /// <summary>
        /// 调用接口凭证
        /// </summary>
        public string access_token { get; set; }
    }
}