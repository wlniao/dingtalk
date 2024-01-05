using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Request
{
    /// <summary>
    /// 发起待办 的请求参数
    /// </summary>
    public class WorkRecordAddRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 待办事项对应的用户id
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 待办时间。Unix时间戳，毫秒级
        /// </summary>
        public long create_time { get; set; }
        /// <summary>
        /// 待办事项的标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 待办事项的跳转链接
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 待办事项表单
        /// </summary>
        public List<Models.FormItemVo> formItemList { get; set; }
        /// <summary>
        /// 调用接口凭证
        /// </summary>
        public string access_token { get; set; }
    }
}