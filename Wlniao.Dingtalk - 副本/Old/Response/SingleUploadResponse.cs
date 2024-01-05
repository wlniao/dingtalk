using System;
using System.Collections.Generic;
namespace Wlniao.Dingtalk.Response
{
    /// <summary>
    /// 单步上传文件 的输出内容
    /// </summary>
    public class SingleUploadResponse : Wlniao.Handler.IResponse
    {
        /// <summary>
        /// 出错返回码，为0表示成功，非0表示调用失败
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 返回码提示语
        /// </summary>
        public string errmsg { get; set; }
        /// <summary>
        /// 媒体文件上传后获取的唯一标识
        /// </summary>
        public string media_id { get; set; }
    }
}
