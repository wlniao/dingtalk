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
            EncoderMap.Add("send_corpmessage", SendCorpMessageEncode);

            DecoderMap.Add("send_corpmessage", SendCorpMessageDecode);
        }

        #region SendCorpMessage
        private void SendCorpMessageEncode(Context ctx)
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
        private void SendCorpMessageDecode(Context ctx)
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