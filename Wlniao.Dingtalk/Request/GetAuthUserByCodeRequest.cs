using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Request
{
    /// <summary>
    /// 获取免登授权码用户详情 的请求参数
    /// </summary>
    public class GetAuthUserByCodeRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 免登授权码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 调用接口凭证
        /// </summary>
        public string access_token { get; set; }
    }
}