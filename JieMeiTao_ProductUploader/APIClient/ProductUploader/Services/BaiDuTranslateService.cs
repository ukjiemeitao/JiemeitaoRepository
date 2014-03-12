using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;
using System.Text;
using System.Net;
using Newtonsoft.Json;

namespace ProductUploader.Services
{
    public class BaiDuTranslateService
    {
        private readonly string apikey = "rcbNTTRwjQxEfLZLz0zDk9fG";
        private readonly string host = "openapi.baidu.com";
        private readonly string path = "/public/2.0/bmt/translate";
        private UriBuilder builder;
        private StringBuilder query;

        public BaiDuTranslateService()
        {
            builder = new UriBuilder();
            query = new StringBuilder();
            builder.Scheme = "http";
            builder.Host = host;
            builder.Path = path;
            query.Append(string.Format("client_id={0}", apikey)); 
        }

        public IList<TransResult> Translate(string source, string from, string to)
        {
            query.Append(string.Format("&q={0}", source));
            query.Append(string.Format("&from={0}", from));
            query.Append(string.Format("&to={0}", to));
            builder.Query = query.ToString();

            WebClient wc = new WebClient();
            var result =  wc.DownloadString(builder.Uri.AbsoluteUri);

            TransResponse response = JsonConvert.DeserializeObject<TransResponse>(result);
           
            return response == null ? null :response.trans_result;
        }

        public class TransResponse
        {
            public string from { get; set; }
            public string to { get; set; }
            public IList<TransResult> trans_result { get; set; }
            public string error_code { get; set; }
            public string error_msg { get; set; }
            public string query { get; set; }
        }

        public class TransResult
        {
            public string src { get; set; }
            public string dst { get; set; }
        }


    }
}