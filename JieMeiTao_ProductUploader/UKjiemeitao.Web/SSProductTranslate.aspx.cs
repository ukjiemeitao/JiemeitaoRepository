using ShopStyleServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SSProductTranslate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            LoadProduct(DateTime.Today, DateTime.Today);
        }
    }

    private void LoadProduct(DateTime startDate, DateTime endDate)
    {
        using (ShopStyleServiceClient s = new ShopStyleServiceClient())
        {
            var p = s.GetProductByID("1e79e3ae-b5af-4f3c-9421-05a63c22157d");

            var datas = s.GetProduct(startDate, endDate);
            this.gvProducts.DataSource = datas;
            this.gvProducts.DataBind();
        }
    }


}