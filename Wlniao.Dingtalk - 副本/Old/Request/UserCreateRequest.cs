using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Request
{
    /// <summary>
    /// 创建用户 的请求参数
    /// </summary>
    public class UserCreateRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 成员名称。 长度为1 ~64个字符
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 手机号码，企业内必须唯一，不可重复。如果是国际号码，请使用+xx-xxxxxx的格式
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 员工在当前企业内的唯一标识，也称staffId。可由企业在创建时指定，并代表一定含义比如工号，创建后不可修改，企业内必须唯一。 长度为1 ~64个字符，如果不传，服务器将自动生成一个userid。
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 职位信息。 长度为0 ~64个字符
        /// </summary>
        public string position { get; set; }
        /// <summary>
        /// 备注，长度为0~1000个字符
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 员工工号。对应显示到OA后台和客户端个人资料的工号栏目。 长度为0 ~64个字符
        /// </summary>
        public string jobnumber { get; set; }
        /// <summary>
        /// 数组类型，数组里面值为整型，成员所属部门id列表
        /// </summary>
        public List<Int32> department { get; set; }
        /// <summary>
        /// 调用接口凭证
        /// </summary>
        public string access_token { get; set; }
    }
}