using System;
namespace Wlniao.Dingtalk
{
    /// <summary>
    /// 
    /// </summary>
    public class Error : Wlniao.Handler.IResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 errcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String errmsg { get; set; }
    }
}