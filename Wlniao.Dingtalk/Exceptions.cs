using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wlniao.Dingtalk.Exceptions
{
    public class ConfigException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public ConfigException(string msg) : base(msg) { }
    }
    public class ResponseException : Exception
    {
        /// <summary>
        /// 内容输出错误
        /// </summary>
        /// <param name="msg"></param>
        public ResponseException(string msg) : base(msg) { }
    }
}