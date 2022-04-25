using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.User
{
    /// <summary>
    /// 根据手机号查询用户
    /// </summary>
    public class GetByMobile : Context
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public GetByMobile(GetByMobileRequest obj)
        {
            base.Method = "POST";
            base.ApiPath = "/topapi/v2/user/getbymobile?access_token=ACCESS_TOKEN";
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
            var res = rlt.data as GetByMobileResponse;
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