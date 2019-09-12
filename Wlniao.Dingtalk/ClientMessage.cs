using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wlniao.Dingtalk.Request;
using Wlniao.Dingtalk.Response;
namespace Wlniao.Dingtalk
{
    /// <summary>
    /// 消息通知
    /// </summary>
    public class ClientMessage : Client
    {
        /// <summary>
        /// 
        /// </summary>
        public ClientMessage()
        {
            this.CorpId = CfgCorpId;
            this.AppKey = CfgAppKey;
            this.AppSecret = CfgAppSecret;
            handler = new ApiHandler();
            handler = new MessageHandler(handler);
            handler = new RetryHandler(handler);
        }
        /// <summary>
        /// 
        /// </summary>
        public ClientMessage(String AppKey, String AppSecret)
        {
            this.AppKey = AppKey;
            this.AppSecret = AppSecret;
            handler = new ApiHandler();
            handler = new MessageHandler(handler);
            handler = new RetryHandler(handler);
        }
        /// <summary>
        /// 
        /// </summary>
        public ClientMessage(String CorpId, String AppKey, String AppSecret, String SuiteTicket)
        {
            this.CorpId = CorpId;
            this.AppKey = AppKey;
            this.AppSecret = AppSecret;
            this.SuiteTicket = SuiteTicket;
            handler = new ApiHandler();
            handler = new MessageHandler(handler);
            handler = new RetryHandler(handler);
        }


        #region SendCorpMessage 发送工作通知消息
        /// <summary>
        /// 发送工作通知消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="agent_id"></param>
        /// <param name="userid_list"></param>
        /// <param name="dept_id_list"></param>
        /// <param name="to_all_user"></param>
        /// <returns></returns>
        public ApiResult<String> SendCorpMessage(Object msg, String agent_id, String userid_list, String dept_id_list = "", Boolean to_all_user = false)
        {
            var res = GetResponseFromAsyncTask(CallAsync<SendCorpMessageRequest, SendCorpMessageResponse>("send_corpmessage", new SendCorpMessageRequest()
            {
                msg = msg,
                agent_id = agent_id,
                userid_list = userid_list,
                dept_id_list = dept_id_list,
                to_all_user = to_all_user,
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
                else if (!string.IsNullOrEmpty(res.data.task_id))
                {
                    rlt.data = res.data.task_id;
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