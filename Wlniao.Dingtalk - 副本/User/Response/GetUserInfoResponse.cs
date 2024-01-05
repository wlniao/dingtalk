using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.User
{
    /// <summary>
    /// 通过免登码获取用户信息 的输出内容
    /// </summary>
    public class GetUserInfoResponse : BaseResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public UserGetByCodeResponse result { get; set; }

        /// <summary>
        /// 返回结果。
        /// </summary>
        public class UserGetByCodeResponse
        {
            /// <summary>
            /// 用户的userid
            /// </summary>
            public string userid { get; set; }
            /// <summary>
            /// 设备ID
            /// </summary>
            public string device_id { get; set; }
            /// <summary>
            /// 是否是管理员
            /// </summary>
            public bool sys { get; set; }
            /// <summary>
            /// 管理员级别 1:主管理员 2:子管理员 100:老板 0：其他（如普通员工）
            /// </summary>
            public int sys_level { get; set; }
            /// <summary>
            /// 用户关联的unionId
            /// </summary>
            public string associated_unionid { get; set; }
            /// <summary>
            /// 用户unionId
            /// </summary>
            public string unionid { get; set; }
            /// <summary>
            /// 用户名字
            /// </summary>
            public string name { get; set; }
        }
    }
}
