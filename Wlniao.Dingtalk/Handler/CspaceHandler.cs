using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Wlniao.Handler;
namespace Wlniao.Dingtalk
{
    /// <summary>
    /// 钉盘接口调用
    /// </summary>
    public class CspaceHandler : DingtalkHandler
    {
        /// <summary>
        /// 
        /// </summary>
        public CspaceHandler(PipelineHandler handler) : base(handler)
        {
            EncoderMap.Add("single_upload", SingleUploadEncode);
            EncoderMap.Add("add_to_single_chat", AddToSingleChatEncode);

            DecoderMap.Add("single_upload", SingleUploadDecode);
            DecoderMap.Add("add_to_single_chat", AddToSingleChatDecode);
        }

        #region SingleUpload
        private void SingleUploadEncode(Context ctx)
        {
            var req = ctx.Request as Request.SingleUploadRequest;
            if (req != null)
            {
                if (string.IsNullOrEmpty(req.agent_id))
                {
                    ctx.Response = new Error() { errmsg = "missing agent_id" };
                    return;
                }
                if (string.IsNullOrEmpty(req.access_token))
                {
                    ctx.Response = new Error() { errmsg = "missing access_token" };
                    return;
                }
                if (req.media == null || req.media.Bytes == null || req.media.Bytes.Length == 0)
                {
                    ctx.Response = new Error() { errmsg = "missing media" };
                    return;
                }
                ctx.Method = System.Net.Http.HttpMethod.Post;
                var contentType = "";
                var texts = new Dictionary<string, string>();
                var files = new Dictionary<string, Models.FileItem>();
                files.Add("file", req.media);
                ctx.HttpRequestBody = Client.DoPost(texts, files, out contentType);
                ctx.HttpRequestHeaders = new Dictionary<string, string>();
                ctx.HttpRequestHeaders.Add("Content-Type", contentType);
                ctx.RequestPath = "/file/upload/single"
                    + "?access_token=" + req.access_token
                    + "&agent_id=" + req.agent_id
                    + "&file_size=" + req.file_size;
            }
        }
        private void SingleUploadDecode(Context ctx)
        {
            try
            {
                ctx.Response = JsonConvert.DeserializeObject<Response.SingleUploadResponse>(ctx.HttpResponseString);
            }
            catch
            {
                ctx.Response = new Error() { errmsg = "InvalidJsonString" };
            }
        }
        #endregion

        #region AddToSingleChat
        private void AddToSingleChatEncode(Context ctx)
        {
            var req = ctx.Request as Request.AddToSingleChatRequest;
            if (req != null)
            {
                if (req.userid == null)
                {
                    ctx.Response = new Error() { errmsg = "missing userid" };
                    return;
                }
                if (string.IsNullOrEmpty(req.agent_id))
                {
                    ctx.Response = new Error() { errmsg = "missing agent_id" };
                    return;
                }
                if (string.IsNullOrEmpty(req.media_id))
                {
                    ctx.Response = new Error() { errmsg = "missing media_id" };
                    return;
                }
                if (string.IsNullOrEmpty(req.file_name))
                {
                    ctx.Response = new Error() { errmsg = "missing file_name" };
                    return;
                }
                if (string.IsNullOrEmpty(req.access_token))
                {
                    ctx.Response = new Error() { errmsg = "missing access_token" };
                    return;
                }
                ctx.Method = System.Net.Http.HttpMethod.Post;
                //var ht = new System.Collections.Hashtable();
                //ht.Add("userid", req.userid);
                //ht.Add("agent_id", req.agent_id);
                //ht.Add("media_id", strUtil.UrlEncode(req.media_id));
                //ht.Add("file_name", strUtil.UrlEncode(req.file_name));
                //ctx.HttpRequestString = JsonConvert.SerializeObject(ht);
                ctx.RequestPath = "/cspace/add_to_single_chat"
                    + "?access_token=" + req.access_token
                    + "&userid=" + req.userid
                    + "&agent_id=" + req.agent_id
                    + "&media_id=" + strUtil.UrlEncode(req.media_id)
                    + "&file_name=" + strUtil.UrlEncode(req.file_name);
            }
        }
        private void AddToSingleChatDecode(Context ctx)
        {
            try
            {
                ctx.Response = JsonConvert.DeserializeObject<Response.AddToSingleChatResponse>(ctx.HttpResponseString);
            }
            catch
            {
                ctx.Response = new Error() { errmsg = "InvalidJsonString" };
            }
        }
        #endregion
    }
}