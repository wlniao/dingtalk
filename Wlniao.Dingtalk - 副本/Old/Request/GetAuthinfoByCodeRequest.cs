using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Request
{
    /// <summary>
    /// 通过临时授权码获取授权用户的个人信息 的请求参数
    /// </summary>
    public class GetAuthinfoByCodeRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 授权登录应用的appId
        /// </summary>
        public string accessKey { get; set; }
        /// <summary>
        /// 授权登录应用的appSecret
        /// </summary>
        public string appSecret { get; set; }
        /// <summary>
        /// 用户授权的临时授权码code，只能使用一次；在前面步骤中跳转到redirect_uri时会追加code参数
        /// </summary>
        public string tmp_auth_code { get; set; }
    }
}