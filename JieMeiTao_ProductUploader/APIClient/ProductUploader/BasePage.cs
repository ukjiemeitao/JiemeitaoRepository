using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ProductUploader
{
    public class BasePage : System.Web.UI.Page
    {
        public static readonly string RedirectUri = ConfigurationManager.AppSettings["Redirect_uri"];
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Request.Params["Pid"] != null && string.IsNullOrEmpty(LoginName))
            {
                Response.Write("{'data':'-2','msg':'" + RedirectUri + "'}");
                Response.End();
            }
            if (string.IsNullOrEmpty(LoginName))
            {
                
                Response.Redirect("Login.aspx");
            }
        }

        public string LoginName
        {
            get
            {
                if (Session.Count > 0 && Session["SessionUsername"] != null)
                {
                    return Session["SessionUsername"].ToString();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}