using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Department
{
    /// <summary>
    /// 获取部门列表
    /// </summary>
    public class ListSubDepartment : Context
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public ListSubDepartment(ListSubDepartmentRequest obj)
        {
            base.Method = "POST";
            base.OldHost = true;
            base.ApiPath = "/topapi/v2/department/listsub?access_token=ACCESS_TOKEN";
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
            var res = rlt.data as ListSubDepartmentResponse;
            if (res == null)
            {
                rlt.message = "内容输出无效";
            }
            else if (res.errcode == "0" && res.result != null)
            {
                rlt.success = true;
            }
            else
            {
                rlt.message = "未查询到相关部门信息";
            }
        }
    }
}