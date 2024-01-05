using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Auth
{
    /// <summary>
    /// 获取第三方应用授权企业的accessToken 的输出内容
    /// </summary>
    public class CorpAccessTokenResponse : BaseResponse
    {
        /// <summary>
        /// 授权企业的accessToken，最长为512字节
        /// </summary>
        public string accessToken { get; set; }
        /// <summary>
        /// 凭证的有效时间（秒）
        /// </summary>
        public int expires_in { get; set; }
    }
}
