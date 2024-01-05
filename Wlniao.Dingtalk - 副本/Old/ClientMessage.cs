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
    public class ClientMessage : ClientOld
    {
        /// <summary>
        /// 
        /// </summary>
        public ClientMessage()
        {
            this.CorpId = CfgCorpId;
            this.AppKey = CfgAppKey;
            this.AppSecret = CfgAppSecret;
            this.ApiServer = CfgApiServer;
            handler = new ApiHandler();
            handler = new MessageHandler(handler);
            handler = new RetryHandler(handler);
        }
        /// <summary>
        /// 
        /// </summary>
        public ClientMessage(String AppKey, String AppSecret, String ApiServer = null)
        {
            this.AppKey = AppKey;
            this.AppSecret = AppSecret;
            this.ApiServer = string.IsNullOrEmpty(ApiServer) ? CfgApiServer : ApiServer;
            handler = new ApiHandler();
            handler = new MessageHandler(handler);
            handler = new RetryHandler(handler);
        }
        /// <summary>
        /// 
        /// </summary>
        public ClientMessage(String CorpId, String AppKey, String AppSecret, String SuiteTicket, String ApiServer = null)
        {
            this.CorpId = CorpId;
            this.AppKey = AppKey;
            this.AppSecret = AppSecret;
            this.SuiteTicket = SuiteTicket;
            this.ApiServer = string.IsNullOrEmpty(ApiServer) ? CfgApiServer : ApiServer;
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


        #region SendCorpMessage 发送普通消息
        /// <summary>
        /// 发送工作通知消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="sender"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        public ApiResult<String> SendToMessage(Object msg, String sender, String cid)
        {
            var res = GetResponseFromAsyncTask(CallAsync<SendToMessageRequest, SendToMessageResponse>("send_tomessage", new SendToMessageRequest()
            {
                cid = cid,
                msg = msg,
                sender = sender,
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
                else if (!string.IsNullOrEmpty(res.data.receiver))
                {
                    rlt.data = res.data.receiver;
                    rlt.success = true;
                }
            }
            return rlt;
        }
        #endregion


    }
}