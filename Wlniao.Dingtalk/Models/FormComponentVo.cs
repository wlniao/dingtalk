using System;
namespace Wlniao.Dingtalk.Models
{
    /// <summary>
    /// 审批流表单参数
    /// </summary>
    public class FormComponentVo
    {
        /// <summary>
        /// 表单每一栏的名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 表单每一栏的值
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 表单内容
        /// </summary>
        public string ext_value { get; set; }
    }
}