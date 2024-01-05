using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wlniao.Dingtalk.User
{
    public class UserInfo
    {
        /// <summary>
        /// 用户名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 用户的userid
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户unionId
        /// </summary>
        public string UnionId { get; set; }
        /// <summary>
        /// 设备ID
        /// </summary>
        public string DeviceId { get; set; }
        /// <summary>
        /// 是否是管理员
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// 管理员级别 1:主管理员 2:子管理员 100:老板 0：其他（如普通员工）
        /// </summary>
        public int SysLevel { get; set; }
        /// <summary>
        /// 用户关联的unionId
        /// </summary>
        public string AssociatedUnionId { get; set; }

        /// <summary>
        /// 免登码获取用户信息
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static ApiResult<UserInfo> GetByCode(Context ctx, String code)
        {
            var response = ctx.Request<Dictionary<String, Object>>(Settings.DingtalkOApi, "/topapi/v2/user/getuserinfo", new { code }, 1);
            var result = new ApiResult<UserInfo> { code = response.code, message = response.message };
            if (response.success)
            {
                var item = response.data;
                result.data = new UserInfo
                {
                    Name = item.GetString("name"),
                    UserId = item.GetString("userid"),
                    UnionId = item.GetString("unionid"),
                    IsAdmin = item.GetBoolean("sys"),
                    SysLevel = item.GetInt32("sys_level"),
                    DeviceId = item.GetString("device_id"),
                    AssociatedUnionId = item.GetString("associated_unionid"),
                };
                result.success = true;
            }
            return result;
        }
    }
}