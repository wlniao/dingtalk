using System;
using System.Collections.Generic;
using System.Text;
using Wlniao.Handler;
namespace Wlniao.Dingtalk
{
    /// <summary>
    /// 
    /// </summary>
    public class RetryHandler : PipelineHandler
    {
        private static int WLN_RETRY_REQUEST = -1;
        /// <summary>
        /// 重试次数
        /// </summary>
        private static int MaxRetryTimes
        {
            get
            {
                if (WLN_RETRY_REQUEST < 0)
                {
                    var _WLN_RETRY_REQUEST = Wlniao.Config.GetSetting("WLN_RETRY_REQUEST");
                    if (string.IsNullOrEmpty(_WLN_RETRY_REQUEST))
                    {
                        WLN_RETRY_REQUEST = 0;
                    }
                    else if (_WLN_RETRY_REQUEST == "true")
                    {
                        WLN_RETRY_REQUEST = 3;
                    }
                    else
                    {
                        WLN_RETRY_REQUEST = cvt.ToInt(_WLN_RETRY_REQUEST);
                    }
                }
                return WLN_RETRY_REQUEST;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public RetryHandler(PipelineHandler handler) : base(handler) { }

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
            var _ctx = (Context)ctx;
            while (true)
            {
                Exception exceptionForRetry = null;
                try
                {
                    inner.HandleAfter(ctx);
                }
                catch (Exception exception)
                {
                    exceptionForRetry = exception;
                }
                if (_ctx.Retry >= MaxRetryTimes)
                {
                    if (exceptionForRetry != null)
                    {
                        throw exceptionForRetry;
                    }
                    return;
                }
                if (exceptionForRetry == null)
                {
                    return;
                }
                _ctx.Retry++;
                System.Threading.Tasks.Task.Delay(1000 * _ctx.Retry).Wait();
                inner.HandleBefore(_ctx);
            }
        }
    }
}