using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Caching;
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
        private const string SearchKey = "SEARCHKEY";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string pid = Request.Params["Pid"];
                string catid = Request.Params["CatId"];
                string fts = Request.Params["Fts"];
                string prosetName = Request.Params["ProSetName"];
                if (!string.IsNullOrWhiteSpace(pid))
                {
                    try
                    {
                        using (var ssService = new ShopStyleService())
                        {
                            var pids = pid.Split(',').Select(m => m.Trim()).ToList();
                            var searchList = Cache[SearchKey] as List<Product>;
                            if (searchList == null)
                            {
                                Response.Write("{'data':'0'}");
                                Response.End();
                            }
                            var selectList = new List<Product>();
                            if (pid.Contains(","))
                            {
                                selectList =
                                    searchList.Where(
                                        m => pids.Contains(m.getId().ToString(CultureInfo.InvariantCulture)))
                                        .ToList();

                            }
                            else
                            {
                                long id;
                                if (long.TryParse(pid, out id))
                                {
                                    selectList = searchList.Where(m => m.getId() == id).ToList();

                                }
                            }
                            if (selectList.Count > 0)
                            {
                                ssService.DownloadProductsByProducts(fts, catid, prosetName,
                                    selectList);
                                Response.Write("{'data':'1'}");
                                return;
                            }
                            else
                            {
                                Response.Write("{'data':'0'}");
                                return;
                            }
                        }
                    }
                    catch (ShopStyle.APIException ex)
                    {
                        string message = ex.getMessage()
                            .Replace("\n", "\\n")
                            .Replace("\r", "")
                            .Replace("'", "\\'");
                        Response.Write("{'data':'-1','msg':'" + message + "'}");
                        return;
                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'");
                        Response.Write("{'data':'-1','msg':'" + message + "'}");
                        return;
                    }
                    finally
                    {
                        Response.End();
                    }

                }
                #region 初始化下拉框数据
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
                #endregion
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
            lsbprice.Items.Clear();
            lsbdiscount.Items.Clear();
            lsbsize.Items.Clear();
            try
            {
                using (var ssService = new ShopStyleService())
                {
                    var list = ssService.SearcProducts(txtfts.Text, ddlCategories.SelectedValue, "b" + ddlBrands.SelectedValue, "r" + ddlRetailers.SelectedValue, lsbprice.SelectedValue, lsbdiscount.SelectedValue, txtProSetName.Text);
                    if (Cache[SearchKey] != null)
                        Cache.Remove(SearchKey);
                    Cache.Add(SearchKey, list, null, DateTime.MaxValue, TimeSpan.FromMinutes(10),
                                CacheItemPriority.High, null);
                    var goods =
                        new Dictionary<long, KeyValuePair<string, string>>();
                    var goodss = new List<Tuple<long, string, string, string>>();

                    foreach (var item in list)
                    {
                        var img = item.getImage().getSizes().values().toArray().OfType<ImageSize>().First(m => m.getSizeName() == ImageSize.SizeName.Medium).getUrl();
                        if (goodss.All(m => m.Item1 != item.getId()))
                            goodss.Add(new Tuple<long, string, string, string>(item.getId(), item.getName(), item.getClickUrl(), img));
                        if (!goods.ContainsKey(item.getId()))
                            goods.Add(item.getId(), new KeyValuePair<string, string>(item.getName(), img));

                    }
                    re_goods.DataSource = goodss;
                    re_goods.DataBind();

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
            catch (Exception ex)
            {
                string message = ex.Message.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + message + "')", true);
            }
        }

    }


}