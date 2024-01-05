using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Todo
{
    /// <summary>
    /// 更新钉钉待办任务
    /// </summary>
    public class UpdateTask : Context
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unionId">当前访问资源所归属用户的unionId，和创建者的unionId保持一致</param>
        /// <param name="taskId">待办ID</param>
        /// <param name="access_token"></param>
        /// <param name="obj"></param>
        public UpdateTask(String unionId, String taskId, String access_token, UpdateTaskRequest obj)
        {
            base.Method = "PUT";
            base.ApiPath = "/v1.0/todo/users/" + unionId + "/tasks/" + taskId;
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
        /// <param name="taskId">待办ID</param>
        /// <param name="access_token"></param>
        /// <param name="operatorId"></param>
        /// <param name="obj"></param>
        public UpdateTask(String unionId, String taskId, String access_token, String operatorId, UpdateTaskRequest obj)
        {
            base.Method = "PUT";
            base.ApiPath = "/v1.0/todo/users/" + unionId + "/tasks/" + taskId + "?operatorId=" + operatorId;
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
            var res = rlt.data as UpdateTaskResponse;
            if (res == null)
            {
                rlt.message = "内容输出无效";
            }
            else
            {
                rlt.success = res.result;
            }
        }
    }
}