using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wlniao.Dingtalk
{
    public class Settings
    {
        /// <summary>
        /// 
        /// </summary>
        public const string DingtalkApi = "https://api.dingtalk.com";
        /// <summary>
        /// 
        /// </summary>
        public const string DingtalkOApi = "https://oapi.dingtalk.com";
        /// <summary>
        /// API访问令牌Header参数名称
        /// </summary>
        public const string AccessTokenHeaderName = "x-acs-dingtalk-access-token";
        /// <summary>
        /// 全局默认Webroxy
        /// </summary>
        public static string Webroxy = Config.GetConfigs("Webroxy");
    }
}
