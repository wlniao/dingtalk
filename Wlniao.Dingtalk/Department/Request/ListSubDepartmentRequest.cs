using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Department
{
    /// <summary>
    /// 获取部门列表 的请求参数
    /// </summary>
    public class ListSubDepartmentRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 父部门ID，根部门ID为1
        /// </summary>
        public string dept_id { get; set; }
        /// <summary>
        /// 通讯录语言 (默认值：zh_CN)
        /// </summary>
        public string language { get; set; }
    }
}