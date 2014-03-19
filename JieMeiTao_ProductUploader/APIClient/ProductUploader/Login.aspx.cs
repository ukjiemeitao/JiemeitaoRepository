using System.Configuration;
using System.Text;
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
        public static readonly string TokenUrl = ConfigurationManager.AppSettings["TokenUrl"];
        public static readonly string OAuthUrl = ConfigurationManager.AppSettings["OAuthUrl"];
        public static readonly string ServerUrl = ConfigurationManager.AppSettings["ServerUrl"];
        public static readonly string Appkey = ConfigurationManager.AppSettings["Appkey"];
        public static readonly string Appsecret = ConfigurationManager.AppSettings["Appsecret"];
        public static readonly string Redirect_uri = ConfigurationManager.AppSettings["Redirect_uri"];
        protected void Page_Load(object sender, EventArgs e)
        {
            var query = new StringBuilder();
            if (!IsPostBack)
            {
                var state = Request.QueryString["state"];
                if (string.IsNullOrWhiteSpace(state))
                {
                    query.Append(OAuthUrl);
                    query.Append(string.Format("?{0}={1}", "client_id", Appkey));
                    query.Append(string.Format("&{0}={1}", "response_type", "code"));
                    query.Append(string.Format("&{0}={1}", "redirect_uri", Redirect_uri));
                    query.Append(string.Format("?{0}={1}", "state", "true")); //如果为true，则为淘宝跳转
                    Response.Redirect(query.ToString());
                }
                else
                {
                    var code = Request.QueryString["code"];
                    if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(state))
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "aa", "alert('获取授权码失败')", true);

                    query.Append(string.Format("{0}={1}", "client_id", Appkey));
                    query.Append(string.Format("&{0}={1}", "client_secret", Appsecret));
                    query.Append(string.Format("&{0}={1}", "grant_type", "authorization_code"));
                    query.Append(string.Format("&{0}={1}", "code", code));
                    query.Append(string.Format("&{0}={1}", "redirect_uri", Redirect_uri));
                    var json = HttpHelper.Post(TokenUrl, query.ToString());
                    if (string.IsNullOrWhiteSpace(json))
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "aa", "alert('获取访问令牌失败')", true);

                    var result = (dynamic)JsonConvert.DeserializeObject(json);
                    Session["SessionKey"] = result.access_token;
                    Session["SessionUsername"] = "Login";

                    Response.Redirect("DownloadSetting.aspx");
                }
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