using System;
namespace Wlniao.Dingtalk.Models
{
    /// <summary>
    /// 部门信息
    /// </summary>
    public class Department
    {
        /// <summary>
        /// 部门id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 父部门id，根部门为1
        /// </summary>
        public int parentid { get; set; }
        /// <summary>
        /// 是否同步创建一个关联此部门的企业群，true表示是，false表示不是
        /// </summary>
        public string createDeptGroup { get; set; }
        /// <summary>
        /// 当群已经创建后，是否有新人加入部门会自动加入该群, true表示是，false表示不是
        /// </summary>
        public bool autoAddUser { get; set; }
        /// <summary>
        /// 部门标识字段，开发者可用该字段来唯一标识一个部门，并与钉钉外部通讯录里的部门做映射
        /// </summary>
        public string sourceIdentifier { get; set; }
    }
}