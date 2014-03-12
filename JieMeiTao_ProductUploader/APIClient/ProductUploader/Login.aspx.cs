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