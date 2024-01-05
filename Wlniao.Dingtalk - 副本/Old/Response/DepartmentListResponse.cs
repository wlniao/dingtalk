using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Response
{
    /// <summary>
    /// 获取部门列表 的输出内容
    /// </summary>
    public class DepartmentListResponse : Wlniao.Handler.IResponse
    {
        /// <summary>
        /// 出错返回码，为0表示成功，非0表示调用失败
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 返回码提示语
        /// </summary>
        public string errmsg { get; set; }
        /// <summary>
        /// 部门列表数据，以部门的order字段从小到大排列
        /// </summary>
        public List<Models.Department> department { get; set; }
    }
}
