using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Request
{
    /// <summary>
    /// 获取企业员工人数 的请求参数
    /// </summary>
    public class GetUserCountRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 0：包含未激活钉钉的人员数量 1：仅含已激活钉钉的人员数量
        /// </summary>
        public int onlyActive { get; set; }
        /// <summary>
        /// 调用接口凭证
        /// </summary>
        public string access_token { get; set; }
    }
}