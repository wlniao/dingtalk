using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Request
{
    /// <summary>
    /// 单步上传文件 的请求参数
    /// </summary>
    public class SingleUploadRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 微应用的agentId
        /// </summary>
        public string agent_id { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public int file_size { get; set; }
        /// <summary>
        /// form-data中媒体文件标识，有filename、filelength、content-type等信息
        /// </summary>
        public Models.FileItem media { get; set; }
        /// <summary>
        /// 调用接口凭证
        /// </summary>
        public string access_token { get; set; }
    }
}