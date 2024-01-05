using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Response
{
    /// <summary>
    /// 通过临时授权码获取授权用户的个人信息 的输出内容
    /// </summary>
    public class GetAuthinfoByCodeResponse : Wlniao.Handler.IResponse
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
        /// 授权用户信息
        /// </summary>
        public Models.AuthInfo user_info { get; set; }
    }
}
