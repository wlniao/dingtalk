using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Request
{
    /// <summary>
    /// 获取部门列表 的请求参数
    /// </summary>
    public class DepartmentListRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 父部门id（如果不传，默认部门为根部门，根部门ID为1）
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 通讯录语言（默认zh_CN，未来会支持en_US）
        /// </summary>
        public string lang { get; set; }
        /// <summary>
        /// 是否递归部门的全部子部门，ISV微应用固定传递false
        /// </summary>
        public string fetch_child { get; set; }
        /// <summary>
        /// 调用接口凭证
        /// </summary>
        public string access_token { get; set; }
    }
}