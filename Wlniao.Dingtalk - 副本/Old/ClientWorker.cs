using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wlniao.Dingtalk.Request;
using Wlniao.Dingtalk.Response;
namespace Wlniao.Dingtalk
{
    /// <summary>
    /// 待办
    /// </summary>
    public class ClientWorker : ClientOld
    {
        /// <summary>
        /// 
        /// </summary>
        public ClientWorker()
        {
            this.CorpId = CfgCorpId;
            this.AppKey = CfgAppKey;
            this.AppSecret = CfgAppSecret;
            this.ApiServer = CfgApiServer;
            handler = new ApiHandler();
            handler = new WorkerHandler(handler);
            handler = new RetryHandler(handler);
        }
        /// <summary>
        /// 
        /// </summary>
        public ClientWorker(String AppKey, String AppSecret, String ApiServer = null)
        {
            this.AppKey = AppKey;
            this.AppSecret = AppSecret;
            this.ApiServer = string.IsNullOrEmpty(ApiServer) ? CfgApiServer : ApiServer;
            handler = new ApiHandler();
            handler = new WorkerHandler(handler);
            handler = new RetryHandler(handler);
        }
        /// <summary>
        /// 
        /// </summary>
        public ClientWorker(String CorpId, String AppKey, String AppSecret, String SuiteTicket, String ApiServer = null)
        {
            this.CorpId = CorpId;
            this.AppKey = AppKey;
            this.AppSecret = AppSecret;
            this.SuiteTicket = SuiteTicket;
            this.ApiServer = string.IsNullOrEmpty(ApiServer) ? CfgApiServer : ApiServer;
            handler = new ApiHandler();
            handler = new WorkerHandler(handler);
            handler = new RetryHandler(handler);
        }



        #region ProcessInstanceCreate 发起审批实例
        /// <summary>
        /// 发起待办
        /// </summary>
        /// <param name="process_code"></param>
        /// <param name="originator_user_id"></param>
        /// <param name="dept_id"></param>
        /// <param name="approvers"></param>
        /// <param name="approvers_v2"></param>
        /// <param name="cc_list"></param>
        /// <param name="cc_position"></param>
        /// <param name="form_component_values"></param>
        /// <param name="agent_id"></param>
        /// <returns></returns>
        public ApiResult<String> ProcessInstanceCreate(String process_code, String originator_user_id, String dept_id, String approvers, List<Models.ProcessInstanceApproverVo> approvers_v2
            , String cc_list, String cc_position, List<Models.FormComponentVo> form_component_values, String agent_id = "")
        {
            var res = GetResponseFromAsyncTask(CallAsync<ProcessInstanceCreateRequest, ProcessInstanceCreateResponse>("processinstance_create", new ProcessInstanceCreateRequest()
            {
                process_code = process_code,
                originator_user_id = originator_user_id,
                dept_id = dept_id,
                approvers = approvers,
                approvers_v2 = approvers_v2,
                cc_list = cc_list,
                cc_position = cc_position,
                form_component_values = form_component_values,
                agent_id = agent_id,
                access_token = string.IsNullOrEmpty(this.SuiteTicket) ? GetToken() : GetCorpToken()
            }, System.Net.Http.HttpMethod.Get));
            var rlt = new ApiResult<String> { message = res.message };
            if (res.success && res.data != null)
            {
                if (res.data.errcode != 0)
                {
                    rlt.code = res.data.errcode.ToString();
                    rlt.message = res.data.errmsg;
                }
                else if (res.data != null && !string.IsNullOrEmpty(res.data.process_instance_id))
                {
                    rlt.data = res.data.process_instance_id;
                    rlt.success = true;
                }
            }
            return rlt;
        }
        #endregion

        #region WorkRecordAdd 发起待办
        /// <summary>
        /// 发起待办
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="title"></param>
        /// <param name="url"></param>
        /// <param name="formItemList"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public ApiResult<String> WorkRecordAdd(String userid, String title, String url, List<Models.FormItemVo> formItemList, Int64 time = 0)
        {
            var res = GetResponseFromAsyncTask(CallAsync<WorkRecordAddRequest, WorkRecordAddResponse>("workrecord_add", new WorkRecordAddRequest()
            {
                userid = userid,
                title = title,
                url = url,
                formItemList = formItemList,
                create_time = time > 0 ? time : DateTools.GetUnix() * 1000,
                access_token = string.IsNullOrEmpty(this.SuiteTicket) ? GetToken() : GetCorpToken()
            }, System.Net.Http.HttpMethod.Get));
            var rlt = new ApiResult<String> { message = res.message };
            if (res.success && res.data != null)
            {
                if (res.data.errcode != 0)
                {
                    rlt.code = res.data.errcode.ToString();
                    rlt.message = res.data.errmsg;
                }
                else if (!string.IsNullOrEmpty(res.data.record_id))
                {
                    rlt.data = res.data.record_id;
                    rlt.success = true;
                }
            }
            return rlt;
        }
        #endregion

        #region WorkRecordUpdate 更新待办
        /// <summary>
        /// 更新待办
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="record_id"></param>
        /// <returns></returns>
        public ApiResult<String> WorkRecordUpdate(String userid, String record_id)
        {
            var res = GetResponseFromAsyncTask(CallAsync<WorkRecordUpdateRequest, WorkRecordUpdateResponse>("workrecord_update", new WorkRecordUpdateRequest()
            {
                userid = userid,
                record_id = record_id,
                access_token = string.IsNullOrEmpty(this.SuiteTicket) ? GetToken() : GetCorpToken()
            }, System.Net.Http.HttpMethod.Get));
            var rlt = new ApiResult<String> { message = res.message };
            if (res.success && res.data != null)
            {
                if (res.data.errcode != 0)
                {
                    rlt.code = res.data.errcode.ToString();
                    rlt.message = res.data.errmsg;
                }
                else if (res.data.result)
                {
                    rlt.data = "true";
                    rlt.success = true;
                    res.message = "更新成功";
                }
                else
                {
                    rlt.data = "false";
                    rlt.success = true;
                    res.message = "更新失败";
                }
            }
            return rlt;
        }
        #endregion


    }
}