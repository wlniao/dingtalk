using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Wlniao.Handler;
namespace Wlniao.Dingtalk
{
    /// <summary>
    /// 消息通知接口调用
    /// </summary>
    public class MessageHandler : DingtalkHandler
    {
        /// <summary>
        /// 
        /// </summary>
        public MessageHandler(PipelineHandler handler) : base(handler)
        {
            EncoderMap.Add("send_tomessage", SendToMessageEncode);
            EncoderMap.Add("send_corpmessage", SendCorpMessageEncode);

            DecoderMap.Add("send_tomessage", SendToMessageDecode);
            DecoderMap.Add("send_corpmessage", SendCorpMessageDecode);
        }

        #region SendToMessage
        private void SendToMessageEncode(ContextOld ctx)
        {
            var req = ctx.Request as Request.SendToMessageRequest;
            if (req != null)
            {
                if (req.msg == null)
                {
                    ctx.Response = new Error() { errmsg = "missing msg" };
                    return;
                }
                if (string.IsNullOrEmpty(req.cid))
                {
                    ctx.Response = new Error() { errmsg = "missing cid" };
                    return;
                }
                if (string.IsNullOrEmpty(req.sender))
                {
                    ctx.Response = new Error() { errmsg = "missing sender" };
                    return;
                }
                if (string.IsNullOrEmpty(req.access_token))
                {
                    ctx.Response = new Error() { errmsg = "missing access_token" };
                    return;
                }
                ctx.Method = System.Net.Http.HttpMethod.Post;
                var ht = new System.Collections.Hashtable();
                ht.Add("msg", req.msg);
                ht.Add("cid", req.cid);
                ht.Add("sender", req.sender);
                ctx.HttpRequestString = JsonConvert.SerializeObject(ht);
                ctx.RequestPath = "/topapi/message/send_to_conversation"
                    + "?access_token=" + req.access_token;
            }
        }
        private void SendToMessageDecode(ContextOld ctx)
        {
            try
            {
                ctx.Response = JsonConvert.DeserializeObject<Response.SendToMessageResponse>(ctx.HttpResponseString);
            }
            catch
            {
                ctx.Response = new Error() { errmsg = "InvalidJsonString" };
            }
        }
        #endregion

        #region SendCorpMessage
        private void SendCorpMessageEncode(ContextOld ctx)
        {
            var req = ctx.Request as Request.SendCorpMessageRequest;
            if (req != null)
            {
                if (req.msg == null)
                {
                    ctx.Response = new Error() { errmsg = "missing msg" };
                    return;
                }
                if (string.IsNullOrEmpty(req.agent_id))
                {
                    ctx.Response = new Error() { errmsg = "missing agent_id" };
                    return;
                }
                if (!req.to_all_user && string.IsNullOrEmpty(req.userid_list) && string.IsNullOrEmpty(req.dept_id_list))
                {
                    ctx.Response = new Error() { errmsg = "missing userid_list" };
                    return;
                }
                if (string.IsNullOrEmpty(req.access_token))
                {
                    ctx.Response = new Error() { errmsg = "missing access_token" };
                    return;
                }
                ctx.Method = System.Net.Http.HttpMethod.Post;
                var ht = new System.Collections.Hashtable();
                ht.Add("msg", req.msg);
                ht.Add("agent_id", req.agent_id);
                ht.Add("userid_list", req.userid_list);
                if (!string.IsNullOrEmpty(req.dept_id_list))
                {
                    ht.Add("dept_id_list", req.dept_id_list);
                }
                if (req.to_all_user)
                {
                    ht.Add("to_all_user", true);
                }
                ctx.HttpRequestString = JsonConvert.SerializeObject(ht);
                ctx.RequestPath = "/topapi/message/corpconversation/asyncsend_v2"
                    + "?access_token=" + req.access_token;
            }
        }
        private void SendCorpMessageDecode(ContextOld ctx)
        {
            try
            {
                ctx.Response = JsonConvert.DeserializeObject<Response.SendCorpMessageResponse>(ctx.HttpResponseString);
            }
            catch
            {
                ctx.Response = new Error() { errmsg = "InvalidJsonString" };
            }
        }
        #endregion
    }
}