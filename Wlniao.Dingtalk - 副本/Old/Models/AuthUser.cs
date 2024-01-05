using System;
namespace Wlniao.Dingtalk.Models
{
    /// <summary>
    /// 授权用户信息
    /// </summary>
    public class AuthUser
    {
        /// <summary>
        /// 员工在当前企业内的唯一标识，也称staffId
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 是否是管理员，true：是，false：不是
        /// </summary>
        public bool is_sys { get; set; }
        /// <summary>
        /// 级别，1：主管理员，2：子管理员，100：老板，0：其他（如普通员工）
        /// </summary>
        public int sys_level { get; set; }
        /// <summary>
        /// 用户的设备id
        /// </summary>
        public string deviceId { get; set; }
    }
}