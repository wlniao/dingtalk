using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Request
{
    /// <summary>
    /// 根据手机号获取员工userid 的请求参数
    /// </summary>
    public class GetUserIdByMobileRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 员工手机号码
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 调用接口凭证
        /// </summary>
        public string access_token { get; set; }
    }
}