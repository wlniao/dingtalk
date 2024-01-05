using System;
namespace Wlniao.Dingtalk.Models
{
    /// <summary>
    /// 授权用户信息
    /// </summary>
    public class AuthInfo
    {
        /// <summary>
        /// 员工名字
        /// </summary>
        public string nick { get; set; }
        /// <summary>
        /// 用户在当前开放应用内的唯一标识
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 用户在当前开放应用所属企业内的唯一标识
        /// </summary>
        public string unionid { get; set; }
    }
}