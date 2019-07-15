using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Wlniao.Handler;
namespace Wlniao.Dingtalk
{
    /// <summary>
    /// 待办 部分接口调用
    /// </summary>
    public class WorkerHandler : DingtalkHandler
    {
        /// <summary>
        /// 
        /// </summary>
        public WorkerHandler(PipelineHandler handler) : base(handler)
        {
            EncoderMap.Add("workrecord_add", WorkRecordAddEncode);
            EncoderMap.Add("workrecord_update", WorkRecordUpdateEncode);
            EncoderMap.Add("processinstance_create", ProcessInstanceCreateEncode);

            DecoderMap.Add("workrecord_add", WorkRecordAddDecode);
            DecoderMap.Add("workrecord_update", WorkRecordUpdateDecode);
            DecoderMap.Add("processinstance_create", ProcessInstanceCreateDecode);
        }

        #region ProcessInstanceCreate
        private void ProcessInstanceCreateEncode(Context ctx)
        {
            var req = ctx.Request as Request.ProcessInstanceCreateRequest;
            if (req != null)
            {
                if (string.IsNullOrEmpty(req.process_code))
                {
                    ctx.Response = new Error() { errmsg = "missing process_code" };
                    return;
                }
                if (string.IsNullOrEmpty(req.originator_user_id))
                {
                    ctx.Response = new Error() { errmsg = "missing originator_user_id" };
                    return;
                }
                if (string.IsNullOrEmpty(req.dept_id))
                {
                    ctx.Response = new Error() { errmsg = "missing dept_id" };
                    return;
                }
                //if (req.formItemList == null || req.formItemList.Count == 0)
                //{
                //    ctx.Response = new Error() { errmsg = "missing formItemList" };
                //    return;
                //}
                if (string.IsNullOrEmpty(req.access_token))
                {
                    ctx.Response = new Error() { errmsg = "missing access_token" };
                    return;
                }
                ctx.Method = System.Net.Http.HttpMethod.Post;
                ctx.HttpRequestString = JsonConvert.SerializeObject(new
                {
                    req.process_code,
                    req.originator_user_id,
                    req.dept_id,
                    req.approvers,
                    req.approvers_v2,
                    req.cc_list,
                    req.cc_position,
                    req.form_component_values,
                    req.agent_id
                });
                ctx.RequestPath = "/topapi/processinstance/create"
                    + "?access_token=" + req.access_token;
            }
        }
        private void ProcessInstanceCreateDecode(Context ctx)
        {
            try
            {
                ctx.Response = JsonConvert.DeserializeObject<Response.ProcessInstanceCreateResponse>(ctx.HttpResponseString);
            }
            catch
            {
                ctx.Response = new Error() { errmsg = "InvalidJsonString" };
            }
        }
        #endregion

        #region WorkRecordAdd
        private void WorkRecordAddEncode(Context ctx)
        {
            var req = ctx.Request as Request.WorkRecordAddRequest;
            if (req != null)
            {
                if (string.IsNullOrEmpty(req.userid))
                {
                    ctx.Response = new Error() { errmsg = "missing userid" };
                    return;
                }
                if (string.IsNullOrEmpty(req.title))
                {
                    ctx.Response = new Error() { errmsg = "missing title" };
                    return;
                }
                if (string.IsNullOrEmpty(req.url))
                {
                    ctx.Response = new Error() { errmsg = "missing url" };
                    return;
                }
                //if (req.formItemList == null || req.formItemList.Count == 0)
                //{
                //    ctx.Response = new Error() { errmsg = "missing formItemList" };
                //    return;
                //}
                if (string.IsNullOrEmpty(req.access_token))
                {
                    ctx.Response = new Error() { errmsg = "missing access_token" };
                    return;
                }
                ctx.Method = System.Net.Http.HttpMethod.Post;
                ctx.HttpRequestString = JsonConvert.SerializeObject(new
                {
                    req.userid,
                    req.create_time,
                    req.title,
                    req.url,
                    req.formItemList
                });
                ctx.RequestPath = "/topapi/workrecord/add"
                    + "?access_token=" + req.access_token;
            }
        }
        private void WorkRecordAddDecode(Context ctx)
        {
            try
            {
                ctx.Response = JsonConvert.DeserializeObject<Response.WorkRecordAddResponse>(ctx.HttpResponseString);
            }
            catch
            {
                ctx.Response = new Error() { errmsg = "InvalidJsonString" };
            }
        }
        #endregion

        #region WorkRecordUpdate
        private void WorkRecordUpdateEncode(Context ctx)
        {
            var req = ctx.Request as Request.WorkRecordUpdateRequest;
            if (req != null)
            {
                if (string.IsNullOrEmpty(req.userid))
                {
                    ctx.Response = new Error() { errmsg = "missing userid" };
                    return;
                }
                if (string.IsNullOrEmpty(req.record_id))
                {
                    ctx.Response = new Error() { errmsg = "missing record_id" };
                    return;
                }
                if (string.IsNullOrEmpty(req.access_token))
                {
                    ctx.Response = new Error() { errmsg = "missing access_token" };
                    return;
                }
                ctx.Method = System.Net.Http.HttpMethod.Post;
                ctx.HttpRequestString = JsonConvert.SerializeObject(new
                {
                    req.userid,
                    req.record_id
                });
                ctx.RequestPath = "/topapi/workrecord/update"
                    + "?access_token=" + req.access_token;
            }
        }
        private void WorkRecordUpdateDecode(Context ctx)
        {
            try
            {
                ctx.Response = JsonConvert.DeserializeObject<Response.WorkRecordUpdateResponse>(ctx.HttpResponseString);
            }
            catch
            {
                ctx.Response = new Error() { errmsg = "InvalidJsonString" };
            }
        }
        #endregion
    }
}