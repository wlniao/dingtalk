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
    public class ClientCspace : Client
    {
        /// <summary>
        /// 
        /// </summary>
        public ClientCspace()
        {
            this.CorpId = CfgCorpId;
            this.AppKey = CfgAppKey;
            this.AppSecret = CfgAppSecret;
            this.ApiServer = CfgApiServer;
            handler = new ApiHandler();
            handler = new CspaceHandler(handler);
            handler = new RetryHandler(handler);
        }
        /// <summary>
        /// 
        /// </summary>
        public ClientCspace(String AppKey, String AppSecret, String ApiServer = null)
        {
            this.AppKey = AppKey;
            this.AppSecret = AppSecret;
            this.ApiServer = string.IsNullOrEmpty(ApiServer) ? CfgApiServer : ApiServer;
            handler = new ApiHandler();
            handler = new CspaceHandler(handler);
            handler = new RetryHandler(handler);
        }
        /// <summary>
        /// 
        /// </summary>
        public ClientCspace(String CorpId, String AppKey, String AppSecret, String SuiteTicket, String ApiServer = null)
        {
            this.CorpId = CorpId;
            this.AppKey = AppKey;
            this.AppSecret = AppSecret;
            this.SuiteTicket = SuiteTicket;
            this.ApiServer = string.IsNullOrEmpty(ApiServer) ? CfgApiServer : ApiServer;
            handler = new ApiHandler();
            handler = new CspaceHandler(handler);
            handler = new RetryHandler(handler);
        }



        #region SingleUpload 单步上传文件
        /// <summary>
        /// 单步上传文件
        /// </summary>
        /// <param name="agent_id"></param>
        /// <param name="media"></param>
        /// <returns></returns>
        public ApiResult<String> SingleUpload(String agent_id, Models.FileItem media)
        {
            var res = GetResponseFromAsyncTask(CallAsync<SingleUploadRequest, SingleUploadResponse>("single_upload", new SingleUploadRequest()
            {
                agent_id = agent_id,
                file_size = media.Bytes.Length,
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

        #region AddToSingleChat 发送钉盘文件给指定用户
        /// <summary>
        /// 发送钉盘文件给指定用户
        /// </summary>
        /// <param name="type"></param>
        /// <param name="media"></param>
        /// <returns></returns>
        public ApiResult<String> AddToSingleChat(String userid, String agent_id, String media_id, String file_name)
        {
            var res = GetResponseFromAsyncTask(CallAsync<AddToSingleChatRequest, AddToSingleChatResponse>("add_to_single_chat", new AddToSingleChatRequest()
            {
                userid = userid,
                agent_id = agent_id,
                media_id = media_id,
                file_name = file_name,
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
                else
                {
                    rlt.data = "";
                    rlt.success = true;
                }
            }
            return rlt;
        }
        #endregion



    }
}