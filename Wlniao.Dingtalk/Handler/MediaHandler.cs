using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Wlniao.Handler;
namespace Wlniao.Dingtalk
{
    /// <summary>
    /// 文件存储 接口调用
    /// </summary>
    public class MediaHandler : DingtalkHandler
    {
        /// <summary>
        /// 
        /// </summary>
        public MediaHandler(PipelineHandler handler) : base(handler)
        {
            EncoderMap.Add("upload", MediaUploadEncode);

            DecoderMap.Add("upload", MediaUploadDecode);
        }

        #region MediaUpload
        private void MediaUploadEncode(Context ctx)
        {
            var req = ctx.Request as Request.MediaUploadRequest;
            if (req != null)
            {
                if (string.IsNullOrEmpty(req.type))
                {
                    ctx.Response = new Error() { errmsg = "missing type" };
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
                texts.Add("type", req.type);
                files.Add("media", req.media);
                ctx.HttpRequestBody = Client.DoPost(texts, files, out contentType);
                ctx.HttpRequestHeaders = new Dictionary<string, string>();
                ctx.HttpRequestHeaders.Add("Content-Type", contentType);
                ctx.RequestPath = "/media/upload"
                    + "?access_token=" + req.access_token;
            }
        }
        private void MediaUploadDecode(Context ctx)
        {
            try
            {
                ctx.Response = JsonConvert.DeserializeObject<Response.MediaUploadResponse>(ctx.HttpResponseString);
            }
            catch
            {
                ctx.Response = new Error() { errmsg = "InvalidJsonString" };
            }
        }
        #endregion
    }
}