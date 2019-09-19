using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Request
{
    /// <summary>
    /// 上传媒体文件 的请求参数
    /// </summary>
    public class MediaUploadRequest : Wlniao.Handler.IRequest
    {
        /// <summary>
        /// 媒体文件类型，分别有图片（image）、语音（voice）、普通文件(file)
        /// </summary>
        public string type { get; set; }
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