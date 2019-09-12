using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Response
{
    /// <summary>
    /// 获取部门详情 的输出内容
    /// </summary>
    public class DepartmentGetResponse : Wlniao.Handler.IResponse
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
        /// 部门id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 在父部门中的排序值，order值小的排序靠前
        /// </summary>
        public string order { get; set; }
        /// <summary>
        /// 父部门id
        /// </summary>
        public string parentid { get; set; }
        /// <summary>
        /// 是否创建一个关联此部门的企业群，默认为false
        /// </summary>
        public bool createDeptGroup { get; set; }
        /// <summary>
        /// 当部门群已经创建后，是否有新人加入部门会自动加入该群，true表示是，false表示不是
        /// </summary>
        public bool autoAddUser { get; set; }
        /// <summary>
        /// 是否隐藏部门, true表示隐藏  false表示显示
        /// </summary>
        public bool deptHiding { get; set; }
        /// <summary>
        /// 可以查看指定隐藏部门的其他部门列表，如果部门隐藏，则此值生效，取值为其他的部门id组成的字符串，使用“|”符号进行分割。总数不能超过200
        /// </summary>
        public string deptPermits { get; set; }
        /// <summary>
        /// 可以查看指定隐藏部门的其他人员列表，如果部门隐藏，则此值生效，取值为其他的人员userid组成的的字符串，使用“|”符号进行分割。总数不能超过200
        /// </summary>
        public string userPermits { get; set; }
        /// <summary>
        /// 限制本部门成员查看通讯录，限制开启后，本部门成员只能看到限定范围内的通讯录。true表示限制开启
        /// </summary>
        public bool outerDept { get; set; }
        /// <summary>
        /// outerDept为true时，可以配置额外可见部门，值为部门id组成的的字符串，使用“|”符号进行分割。总数不能超过200
        /// </summary>
        public string outerPermitDepts { get; set; }
        /// <summary>
        /// outerDept为true时，可以配置额外可见人员，值为userid组成的的字符串，使用“|”符号进行分割。总数不能超过200
        /// </summary>
        public string outerPermitUsers { get; set; }
        /// <summary>
        /// 企业群群主
        /// </summary>
        public string orgDeptOwner { get; set; }
        /// <summary>
        /// outerDept为true时，可以配置该字段，为true时，表示只能看到所在部门及下级部门通讯录
        /// </summary>
        public string outerDeptOnlySelf { get; set; }
        /// <summary>
        /// 部门的主管列表，取值为由主管的userid组成的字符串，不同的userid使用“\|”符号进行分割
        /// </summary>
        public string deptManagerUseridList { get; set; }
        /// <summary>
        /// 部门标识字段，开发者可用该字段来唯一标识一个部门，并与钉钉外部通讯录里的部门做映射
        /// </summary>
        public string sourceIdentifier { get; set; }
    }
}
