using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top.Api;

namespace UKjiemeitao.Application.Implementation
{
    /// <summary>
    /// 获取ITopClient
    /// </summary>
    public class TopClientService
    {
        private TopClientService()
        {
        }

        private const string serverurl = "http://gw.api.taobao.com/router/rest";
        private const string appkey = "21682361";
        private const string appsecret = "e5600d5fedaf052ebda7a62d5b074b6d";
        private const string sessionkey = "6102128992bb5eb347d10eb85de4ae537de013d2f849666211999340";
        private static ITopClient itopclient = null;

        public static ITopClient GetTopClient()
        {
            if (itopclient == null)
            {
                itopclient = new DefaultTopClient(serverurl, appkey, appsecret);
            }
            return itopclient;
        }
    }
}
