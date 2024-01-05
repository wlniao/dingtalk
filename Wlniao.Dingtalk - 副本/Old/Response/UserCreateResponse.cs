using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Response
{
    /// <summary>
    /// 创建用户 的输出内容
    /// </summary>
    public class UserCreateResponse : Wlniao.Handler.IResponse
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
        /// 员工唯一标识
        /// </summary>
        public string userid { get; set; }
    }
}
