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
    public class ClientMedia : Client
    {
        /// <summary>
        /// 
        /// </summary>
        public ClientMedia()
        {
            this.CorpId = CfgCorpId;
            this.AppKey = CfgAppKey;
            this.AppSecret = CfgAppSecret;
            this.ApiServer = CfgApiServer;
            handler = new ApiHandler();
            handler = new MediaHandler(handler);
            handler = new RetryHandler(handler);
        }
        /// <summary>
        /// 
        /// </summary>
        public ClientMedia(String AppKey, String AppSecret, String ApiServer = null)
        {
            this.AppKey = AppKey;
            this.AppSecret = AppSecret;
            this.ApiServer = string.IsNullOrEmpty(ApiServer) ? CfgApiServer : ApiServer;
            handler = new ApiHandler();
            handler = new MediaHandler(handler);
            handler = new RetryHandler(handler);
        }
        /// <summary>
        /// 
        /// </summary>
        public ClientMedia(String CorpId, String AppKey, String AppSecret, String SuiteTicket, String ApiServer = null)
        {
            this.CorpId = CorpId;
            this.AppKey = AppKey;
            this.AppSecret = AppSecret;
            this.SuiteTicket = SuiteTicket;
            this.ApiServer = string.IsNullOrEmpty(ApiServer) ? CfgApiServer : ApiServer;
            handler = new ApiHandler();
            handler = new MediaHandler(handler);
            handler = new RetryHandler(handler);
        }


        #region MediaUpload 上传媒体文件
        /// <summary>
        /// 上传媒体文件
        /// </summary>
        /// <param name="type"></param>
        /// <param name="media"></param>
        /// <returns></returns>
        public ApiResult<String> MediaUpload(String type, Models.FileItem media)
        {
            var res = GetResponseFromAsyncTask(CallAsync<MediaUploadRequest, MediaUploadResponse>("upload", new MediaUploadRequest()
            {
                type = type,
                media = media,
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
                else if (!string.IsNullOrEmpty(res.data.media_id))
                {
                    rlt.data = res.data.media_id;
                    rlt.success = true;
                }
            }
            return rlt;
        }
        #endregion



    }
}