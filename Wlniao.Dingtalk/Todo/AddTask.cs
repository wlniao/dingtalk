using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Todo
{
    /// <summary>
    /// 新增钉钉待办任务
    /// </summary>
    public class AddTask : Context
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unionId">当前访问资源所归属用户的unionId，和创建者的unionId保持一致</param>
        /// <param name="obj"></param>
        public AddTask(String unionId, String access_token, AddTaskRequest obj)
        {
            obj.creatorId = unionId;
            base.Method = "POST";
            base.ApiPath = "/v1.0/todo/users/" + unionId + "/tasks";
            base.RequestBody = obj;
            base.TokenRequired = true;
            base.HttpRequestHeaders = new Dictionary<string, string>
            {
                { "x-acs-dingtalk-access-token", access_token}
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unionId"></param>
        /// <param name="operatorId"></param>
        /// <param name="obj"></param>
        public AddTask(String unionId, String access_token, String operatorId, AddTaskRequest obj)
        {
            obj.creatorId = unionId;
            base.Method = "POST";
            base.ApiPath = "/v1.0/todo/users/" + unionId + "/tasks?operatorId=" + operatorId;
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
            var res = rlt.data as AddTaskResponse;
            if (res == null)
            {
                rlt.message = "内容输出无效";
            }
            else if (!string.IsNullOrEmpty(res.id))
            {
                rlt.success = true;
            }
        }
    }
}