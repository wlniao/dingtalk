using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Wlniao.Dingtalk
{
    /// <summary>
    /// 请求线程
    /// </summary>
    public abstract class Context : Wlniao.Handler.Context
    {
        /// <summary>
        /// 
        /// </summary>
        public Context(): base()   
        {
            base.Method = this.Method;
            base.ApiHost = this.ApiHost;
            base.ApiPath = this.ApiPath;
            base.Encoding = this.Encoding;
            base.RequestBody = this.RequestBody;
            base.ContentType = this.ContentType;
        }
        /// <summary>
        /// 接口凭据是否为必须
        /// </summary>
        internal protected bool TokenRequired { get; set; }
        /// <summary>
        /// 输出检查方法
        /// </summary>
        public abstract void CheckRespose<TResponse>(ApiResult<TResponse> rlt) where TResponse : Wlniao.Handler.IResponse;
    }
}