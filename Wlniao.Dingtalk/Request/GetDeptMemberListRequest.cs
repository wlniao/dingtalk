using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Request
{
    /// <summary>
    /// 获取部门用户详情 的请求参数
    /// </summary>
    public class GetDeptMemberListRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 通讯录语言（默认zh_CN，未来会支持en_US）
        /// </summary>
        public string lang { get; set; }
        /// <summary>
        /// 获取的部门id
        /// </summary>
        public string department_id { get; set; }
        /// <summary>
        /// 支持分页查询，与size参数同时设置时才生效，此参数代表偏移量,偏移量从0开始
        /// </summary>
        public long offset { get; set; }
        /// <summary>
        /// 支持分页查询，与offset参数同时设置时才生效，此参数代表分页大小，最大100
        /// </summary>
        public long size { get; set; }
        /// <summary>
        /// 支持分页查询，部门成员的排序规则，默认 是按自定义排序； entry_asc：代表按照进入部门的时间升序，entry_desc：代表按照进入部门的时间降序，modify_asc：代表按照部门信息修改时间升序，modify_desc：代表按照部门信息修改时间降序，custom：代表用户定义(未定义时按照拼音)排序
        /// </summary>
        public string order { get; set; }
        /// <summary>
        /// 调用接口凭证
        /// </summary>
        public string access_token { get; set; }
    }
}