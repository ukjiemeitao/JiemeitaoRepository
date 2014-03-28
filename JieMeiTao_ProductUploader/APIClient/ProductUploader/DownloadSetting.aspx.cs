using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProductUploader.DAL;
using ProductUploader.Services;
using com.shopstyle.api;
using com.shopstyle.bo;


namespace ProductUploader
{
    public partial class DownloadSetting : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (CatalogDataContext dct = new CatalogDataContext())
                {
                    ddlBrands.DataSource = dct.SS_Brands.OrderBy(b => b.brand_name);
                    ddlBrands.DataTextField = "brand_name";
                    ddlBrands.DataValueField = "brand_id";
                    ddlBrands.DataBind();
                    ddlBrands.Items.Insert(0, new ListItem("", ""));


                    ddlCategories.DataSource = dct.SS_Categories.OrderBy(c => c.cat_id);
                    ddlCategories.DataTextField = "cat_id";
                    ddlCategories.DataValueField = "cat_id";
                    ddlCategories.DataBind();
                    ddlCategories.Items.Insert(0, new ListItem("", ""));

                    ddlRetailers.DataSource = dct.SS_Retailers.OrderBy(r => r.name);
                    ddlRetailers.DataTextField = "name";
                    ddlRetailers.DataValueField = "retailer_id";
                    ddlRetailers.DataBind();
                    ddlRetailers.Items.Insert(0, new ListItem("", ""));

                    ddlColors.DataSource = dct.SS_Colors.OrderBy(c => c.color_name);
                    ddlColors.DataTextField = "color_name";
                    ddlColors.DataValueField = "color_id";
                    ddlColors.DataBind();
                    ddlRetailers.Items.Insert(0, new ListItem("", ""));
                }

            }

        }

        protected void btngetpriceanddiscount_Click(object sender, EventArgs e)
        {
            lsbprice.Items.Clear();
            lsbdiscount.Items.Clear();
            lsbsize.Items.Clear();

            try
            {
                using (ShopStyleService ssService = new ShopStyleService())
                {
                    List<SS_Discount> dcList;
                    List<SS_Price> pList = ssService.GetPriceAndDiscountRange(ddlCategories.SelectedValue, "b" + ddlBrands.SelectedValue, "r" + ddlRetailers.SelectedValue, out dcList);

                    if (pList != null && pList.Count > 0)
                    {
                        lsbprice.DataTextField = "Name";
                        lsbprice.DataValueField = "FilterID";
                        lsbprice.DataSource = pList;
                        lsbprice.DataBind();

                    }

                    if (dcList != null && dcList.Count > 0)
                    {
                        lsbdiscount.DataTextField = "Name";
                        lsbdiscount.DataValueField = "FilterID";
                        lsbdiscount.DataSource = dcList;
                        lsbdiscount.DataBind();
                    }

                    if (!string.IsNullOrEmpty(ddlCategories.SelectedValue))
                    {
                        List<SS_Size> sList = ssService.GetSizesByCategory(ddlCategories.SelectedValue);
                        lsbsize.DataTextField = "name";
                        lsbsize.DataValueField = "size_id";
                        lsbsize.DataSource = sList;
                        lsbsize.DataBind();
                    }
                }
            }
            catch (ShopStyle.APIException ex)
            {
                string message = ex.getMessage().Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + message + "')", true);
            }
            catch (Exception ex)
            {
                string message = ex.Message.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + message + "')", true);
            }

        }

        protected void btndownload_Click(object sender, EventArgs e)
        {
            try
            {
                using (ShopStyleService ssService = new ShopStyleService())
                {
                    ssService.DownloadProducts(txtfts.Text, ddlCategories.SelectedValue, "b" + ddlBrands.SelectedValue, "r" + ddlRetailers.SelectedValue, lsbprice.SelectedValue, lsbdiscount.SelectedValue, txtProSetName.Text);
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + message + "')", true);
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (ShopStyleService ssService = new ShopStyleService())
                {
                    var list = ssService.SearcProducts(txtfts.Text, ddlCategories.SelectedValue, "b" + ddlBrands.SelectedValue, "r" + ddlRetailers.SelectedValue, lsbprice.SelectedValue, lsbdiscount.SelectedValue, txtProSetName.Text);
                    Dictionary<long, KeyValuePair<string, string>> goods =
                        new Dictionary<long, KeyValuePair<string, string>>();
                    foreach (var item in list)
                    {
                        if (!goods.ContainsKey(item.getId()))
                            goods.Add(item.getId(), new KeyValuePair<string, string>(item.getName(), item.getClickUrl()));
                    }
                    re_goods.DataSource = goods;
                    re_goods.DataBind();

                }
            }
            catch (Exception ex)
            {
                string message = ex.Message.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + message + "')", true);
            }
        }

    }


}