using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top.Api;

namespace ProductUploader.Services
{
    /// <summary>
    /// 获取ITopClient
    /// </summary>
    public class TopClientService
    {
        private TopClientService()
        {
        }

        //private const string serverurl = "http://gw.api.taobao.com/router/rest";
        //private const string appkey = "21734965"; //"21682361";
        //private const string appsecret = "4b3f877f7fcb736f588f3e9d4ed33cbe"; //"e5600d5fedaf052ebda7a62d5b074b6d";
        public static readonly string Appkey = ConfigurationManager.AppSettings["Appkey"];
        public static readonly string Appsecret = ConfigurationManager.AppSettings["Appsecret"];
        public static readonly string ServerUrl = ConfigurationManager.AppSettings["ServerUrl"];

        private static ITopClient _itopclient = null;

        public static ITopClient GetTopClient()
        {
            return _itopclient ?? (_itopclient = new DefaultTopClient(ServerUrl, Appkey, Appsecret));
        }
    }
}
