using System.Text;
using Newtonsoft.Json;
using ProductUploader.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProductUploader
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var query = new StringBuilder();
            //if (!IsPostBack)
            //{
            //    var state = Request.QueryString["state"];
            //    if (string.IsNullOrWhiteSpace(state))
            //    {
            //        query.Append("https://oauth.tbsandbox.com/authorize");//沙箱地址
            //        //query.Append("https://oauth.taobao.com/authorize");//正式地址
            //        query.Append(string.Format("?{0}={1}", "client_id", "1021693615"));
            //        query.Append(string.Format("&{0}={1}", "response_type", "code"));
            //        query.Append(string.Format("&{0}={1}", "redirect_uri", "http://w3.jiemeitao.com/login.aspx"));//上线之后改为 淘宝地址
            //        //query.Append(string.Format("&{0}={1}", "redirect_uri", "http://localhost:2823/Login.aspx"));
            //        query.Append(string.Format("?{0}={1}", "state", "true")); //如果为true，则为淘宝跳转

            //        Response.Redirect(query.ToString());
            //    }
            //    else
            //    {
            //        var code = Request.QueryString["code"];
            //        if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(state))
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "aa", "alert('获取授权码失败')", true);

            //        query.Append(string.Format("{0}={1}", "client_id", "1021693615"));
            //        query.Append(string.Format("&{0}={1}", "client_secret", "sandbox23de48a989acaacaf0ca69b86"));
            //        query.Append(string.Format("&{0}={1}", "grant_type", "authorization_code"));
            //        query.Append(string.Format("&{0}={1}", "code", code));
            //        query.Append(string.Format("&{0}={1}", "redirect_uri", "http://w3.jiemeitao.com/login.aspx"));
            //        //query.Append(string.Format("&{0}={1}", "redirect_uri", "http://localhost:2823/Login.aspx"));
            //        //var json = HttpHelper.Post("https://oauth.taobao.com/token", query.ToString());//正式地址
            //        var json = HttpHelper.Post("https://oauth.tbsandbox.com/token", query.ToString());
            //        if (string.IsNullOrWhiteSpace(json))
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "aa", "alert('获取访问令牌失败')", true);

            //        var result = (dynamic)JsonConvert.DeserializeObject(json);
            //        Session["SessionKey"] = result.access_token;


            //        Response.Redirect("DownloadSetting.aspx");
            //    }
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