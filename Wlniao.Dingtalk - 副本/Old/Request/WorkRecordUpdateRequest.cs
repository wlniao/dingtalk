using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Request
{
    /// <summary>
    /// 更新待办 的请求参数
    /// </summary>
    public class WorkRecordUpdateRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 待办事项对应的用户id
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 待办事项唯一id
        /// </summary>
        public string record_id { get; set; }
        /// <summary>
        /// 调用接口凭证
        /// </summary>
        public string access_token { get; set; }
    }
}