using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.User
{
    /// <summary>
    /// 查询用户详情
    /// </summary>
    public class GetUser : Context
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public GetUser(GetUserRequest obj)
        {
            base.Method = "POST";
            base.OldHost = true;
            base.ApiPath = "/topapi/v2/user/get?access_token=ACCESS_TOKEN";
            base.RequestBody = obj;
            base.TokenRequired = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="rlt"></param>
        public override void CheckRespose<TResponse>(ApiResult<TResponse> rlt)
        {
            var res = rlt.data as GetUserResponse;
            if (res == null)
            {
                rlt.message = "内容输出无效";
            }
            else if (res.errcode == "0" && res.result != null)
            {
                if (string.IsNullOrEmpty(res.result.userid))
                {
                    rlt.message = "未查询到相关用户信息";
                }
                else
                {
                    rlt.success = true;
                }
            }
        }
    }
}