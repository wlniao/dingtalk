using System;
using System.Collections.Generic;
using System.Text;
using Wlniao.Handler;
namespace Wlniao.Dingtalk
{
    /// <summary>
    /// 
    /// </summary>
    public class Handler : PipelineHandler
    {
        /// <summary>
        /// 
        /// </summary>
        public Handler()
        {
            PipelineHandler handler;
            handler = new ApiHandler();
            handler = new DingtalkHandler(handler);
            handler = new RetryHandler(handler);
            inner = handler;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        public override void HandleBefore(IContext ctx)
        {
            inner.HandleBefore(ctx);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        public override void HandleAfter(IContext ctx)
        {
            inner.HandleAfter(ctx);
        }
    }
}