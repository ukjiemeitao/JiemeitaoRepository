using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductUploader
{
    public class BasePage: System.Web.UI.Page
    {

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

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