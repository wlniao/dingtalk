using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Department
{
    /// <summary>
    /// 获取部门详情
    /// </summary>
    public class GetDepartment : Context
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public GetDepartment(GetDepartmentRequest obj)
        {
            base.Method = "POST";
            base.OldHost = true;
            base.ApiPath = "/topapi/v2/department/get?access_token=ACCESS_TOKEN";
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
            var res = rlt.data as GetDepartmentResponse;
            if (res == null)
            {
                rlt.message = "内容输出无效";
            }
            else if (res.errcode == "0" && res.result != null)
            {
                if (string.IsNullOrEmpty(res.result.dept_id))
                {
                    rlt.message = "未查询到相关部门信息";
                }
                else
                {
                    rlt.success = true;
                }
            }
        }
    }
}