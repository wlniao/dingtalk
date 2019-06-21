using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Response
{
    /// <summary>
    /// 获取免登授权码用户详情 的输出内容
    /// </summary>
    public class GetAuthUserByCodeResponse : Wlniao.Handler.IResponse
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
