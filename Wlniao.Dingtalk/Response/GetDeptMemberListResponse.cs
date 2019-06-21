using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Response
{
    /// <summary>
    /// 获取部门用户详情 的输出内容
    /// </summary>
    public class GetDeptMemberListResponse : Wlniao.Handler.IResponse
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
        /// 在分页查询时返回，代表是否还有下一页更多数据
        /// </summary>
        public Boolean hasMore { get; set; }
        /// <summary>
        /// 成员列表
        /// </summary>
        public List<Models.User> userlist { get; set; }
    }
}
