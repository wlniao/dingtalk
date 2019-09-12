using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Request
{
    /// <summary>
    /// 获取部门详情 的请求参数
    /// </summary>
    public class DepartmentGetRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 部门id，根部门id为1
        /// </summary>
        public string id { get; set; }
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