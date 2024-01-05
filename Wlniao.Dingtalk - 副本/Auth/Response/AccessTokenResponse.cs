using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Auth
{
    /// <summary>
    /// 获取access_token 的输出内容
    /// </summary>
    public class AccessTokenResponse : BaseResponse
    {
        /// <summary>
        /// 获取到的凭证，最长为512字节
        /// </summary>
        public string accessToken { get; set; }
        /// <summary>
        /// 凭证的有效时间（秒）
        /// </summary>
        public int expiresIn { get; set; }
    }
}
