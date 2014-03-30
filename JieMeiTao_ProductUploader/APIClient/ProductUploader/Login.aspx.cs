using System.Configuration;
using System.Text;
using System.Web.Caching;
using Newtonsoft.Json;
using ProductUploader.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProductUploader.Services;

namespace ProductUploader
{
    public partial class login : System.Web.UI.Page
    {
        private const string SESSIONKEY = "SESSIONKEY";
        public static readonly string TokenUrl = ConfigurationManager.AppSettings["TokenUrl"];
        public static readonly string OAuthUrl = ConfigurationManager.AppSettings["OAuthUrl"];
        public static readonly string ServerUrl = ConfigurationManager.AppSettings["ServerUrl"];
        public static readonly string Appkey = ConfigurationManager.AppSettings["Appkey"];
        public static readonly string Appsecret = ConfigurationManager.AppSettings["Appsecret"];
        public static readonly string RedirectUri = ConfigurationManager.AppSettings["Redirect_uri"];
        protected void Page_Load(object sender, EventArgs e)
        {
            var query = new StringBuilder();
            if (!IsPostBack)
            {
                //如果返回值有Code
                var code = Request.QueryString["code"];
                if (!string.IsNullOrWhiteSpace(code))
                {
                    if (string.IsNullOrWhiteSpace(code))
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "aa", "alert('获取授权码失败')", true);

                    query.Append(string.Format("{0}={1}", "client_id", Appkey));
                    query.Append(string.Format("&{0}={1}", "client_secret", Appsecret));
                    query.Append(string.Format("&{0}={1}", "grant_type", "authorization_code"));
                    query.Append(string.Format("&{0}={1}", "code", code));
                    query.Append(string.Format("&{0}={1}", "redirect_uri", RedirectUri));
                    var json = HttpHelper.Post(TokenUrl, query.ToString());
                    if (string.IsNullOrWhiteSpace(json))
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "aa", "alert('获取访问令牌失败')", true);

                    var result = (dynamic)JsonConvert.DeserializeObject(json);
                    Session["SessionKey"] = result.access_token;
                    Session["SessionUsername"] = "Login";
                    if (Cache[SESSIONKEY] != null)
                        Cache.Remove(SESSIONKEY);
                    Cache.Add(SESSIONKEY, result.access_token, null, DateTime.MaxValue, TimeSpan.FromMinutes(60),
                        CacheItemPriority.High, null);

                    Response.Redirect("DownloadSetting.aspx");

                }
                var errorStr = Request.QueryString["error"];
                if (!string.IsNullOrWhiteSpace(errorStr))
                {
                    var description = Request.QueryString["error_description"];
                    if (string.IsNullOrWhiteSpace(description))
                        description = "淘宝报错了！重新登录一下吧";
                    Response.Write(string.Format("error_description:{0},description:{1}", errorStr, description));
                    Response.End();
                }

                query.Append(OAuthUrl);
                query.Append(string.Format("?{0}={1}", "client_id", Appkey));
                query.Append(string.Format("&{0}={1}", "response_type", "code"));
                query.Append(string.Format("&{0}={1}", "redirect_uri", RedirectUri));
                Response.Redirect(query.ToString());
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            using (CatalogDataContext dct = new CatalogDataContext())
            {
                var u = dct.Users.SingleOrDefault(o => o.Name == tbName.Text && o.Password == tbPassword.Text);

                if (u != null)
                {
                    Session["SessionUsername"] = u.Name;
                    Response.Redirect("DownloadSetting.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "aa", "alert('用户名或者密码不正确')", true);
                }

            }
        }
    }
}