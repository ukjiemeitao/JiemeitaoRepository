using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProductUploader.Services;
using ProductUploader.DAL;
using com.shopstyle.api;
using com.shopstyle.bo;
using System.Collections;


namespace ProductUploader
{
    public partial class UploadSetting : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {


                populateDropdownList(null);
                applyFilters();

            }

        }
        public long SelectedProductID { get; set; }

        private void populateDropdownList(DateTime? dt)
        {
            using (CatalogDataContext dct = new CatalogDataContext())
            {
                
                ddlProductSet.DataSource = dt == null ? dct.SS_Product_Sets : dct.SS_Product_Sets.Where(p => ((DateTime)p.datetimecreated).Date == dt.Value.Date)   ;
                ddlProductSet.DataTextField = "product_set_name";
                ddlProductSet.DataValueField = "id";
                ddlProductSet.DataBind();
                ddlProductSet.Items.Insert(0, new ListItem("--全部--", "NA"));
            }
        }

        protected void btnInitTBItemCat_Click(object sender, EventArgs e)
        {
            TBProductUploadService tbService = new TBProductUploadService();
            tbService.InitializationItemCat();
        }

        protected void btnInitTBItemProp_Click(object sender, EventArgs e)
        {
            TBProductUploadService tbService = new TBProductUploadService();
            tbService.InitializationItemProp();
        }

        protected void btnInitTBItemPropValue_Click(object sender, EventArgs e)
        {
            TBProductUploadService tbService = new TBProductUploadService();
            tbService.InitializationPropValue();
        }

        protected void txtDownloadDate_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDownloadDate.Text))
            {
                populateDropdownList(DateTime.Parse(txtDownloadDate.Text));
                applyFilters();
            }
            
        }

        protected void ddlProductSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            applyFilters();

        }

        protected void ddlProdctState_SelectedIndexChanged(object sender, EventArgs e)
        {
            applyFilters();
        }

        protected void gridProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            using (CatalogDataContext dct = new CatalogDataContext())
            {
                gridProducts.PageIndex = e.NewPageIndex;

                //gridProducts.DataSource = from product in dct.SS_Products  select new { ID = product.product_id, Name = product.name, Brand = product.SS_Brand.brand_name, Retailer = product.SS_Retailer.name, Price = product.price_label, Sale = product.sale_price_label, IsInStock = product.in_stock, IsTranslated = product.istranslated, ProductSet = product.product_set_name, ProductImage = (from image in dct.SS_Images where image.image_id == product.image_id && image.size_name == "Small" select image.url ).FirstOrDefault() };
                //gridProducts.DataBind();
                applyFilters();
            }
        }

        protected void gridProducts_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
        {
            // Get the datakey of the selected row
            var id = Convert.ToInt32(gridProducts.DataKeys[e.NewSelectedIndex].Value);
            string url = "translateproductdetail.aspx?product_id=";
            string s = "window.open('" + url + id + "', 'popup_window', 'width=800,height=800,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsPassValidation())
                {
                    TBProductUploadService tbService = new TBProductUploadService();

                    tbService.UploadProduct(ddlProductSet.SelectedItem.Text);
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message.Replace("\n", "\\n").Replace("\r", "");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + message + "')", true);
            }

        }

        private void applyFilters()
        {
            try
            {
                using (CatalogDataContext dct = new CatalogDataContext())
                {
                    var products = from product in dct.SS_Products where !dct.TB_Products.Any(tp => tp.IsUploaded == true && tp.SSProductID == product.product_id) select product;

                    if (ddlProductSet.SelectedValue != "NA")
                    {
                        products = from product in products where product.product_set_name_id == new Guid(ddlProductSet.SelectedValue) select product;
                    }

                    if (ddlProdctState.SelectedValue != "NA")
                    {
                        var trans = bool.Parse(ddlProdctState.SelectedValue);
                        if (trans)
                        {
                            products = products.Where(o => o.istranslated.Value == true);
                        }
                        else
                        {
                            products = products.Where(o => o.istranslated.Value == false || o.istranslated == null);
                        }
                    }

                    if (!string.IsNullOrEmpty(txtDownloadDate.Text))
                    {
                        DateTime dt = Convert.ToDateTime(txtDownloadDate.Text);
                        products = from product in products where product.datetimecreated.Value.Date == dt.Date select product;
                    }


                    gridProducts.DataSource = from product in products select new { ID = product.product_id, Name = product.name, Brand = product.SS_Brand.brand_name, Retailer = product.SS_Retailer.name, Price = product.price_label, Sale = product.sale_price_label, IsInStock = product.in_stock, IsTranslated = product.istranslated, ProductSet = product.SS_Product_Set.product_set_name, ProductImage = (from image in dct.SS_Images where image.image_id == product.image_id && image.size_name == "Small" select image.url).FirstOrDefault() };
                    gridProducts.DataBind();


                }
            }
            catch (Exception ex)
            {
                string message = ex.Message.Replace("\n", "\\n").Replace("\r", "");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + message + "')", true);
            }

        }

        private bool IsPassValidation()
        {
            try
            {
                if (ddlProductSet.SelectedValue == "NA")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "infoMessage", "alert('请您选择一个产品组')", true);
                    return false;
                }

                using (CatalogDataContext dct = new CatalogDataContext())
                {
                    var p = dct.SS_Products.Where(o => o.product_set_name_id == new Guid(ddlProductSet.SelectedValue));
                    var isTranNum = p.Count(o => o.istranslated.Value == true);
                    if (p.Count() == isTranNum)
                    {
                        return true;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "infoMessage", "alert('需要把商品组下面的所有商品全部翻译才可上传')", true);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {

                string message = ex.Message.Replace("\n", "\\n").Replace("\r", "");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + message + "')", true);
            }

            return false;
        }

        protected void gridProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                foreach (DictionaryEntry kv in e.Keys)
                {
                    var productId = kv.Value;
                    using (CatalogDataContext dct = new CatalogDataContext())
                    {
                        var ssProduct = dct.SS_Products.FirstOrDefault(p => p.product_id == long.Parse(productId.ToString()));


                        if (ssProduct != null)
                        {
                            dct.SS_Products.DeleteOnSubmit(ssProduct);

                            //delete images, no foreign key cascade
                            foreach (var m in ssProduct.SS_Product_Color_Image_Mappings)
                            {
                                var images = dct.SS_Images.Where(i => i.image_id == m.image_id);

                                foreach (var image in images)
                                {
                                    dct.SS_Images.DeleteOnSubmit(image);
                                }


                            }
                        }
                        dct.SubmitChanges();
                    }
                }

                applyFilters();
            }
            catch (Exception ex)
            {

                string message = ex.Message.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + message + "')", true);
            }


        }

        protected void gridProducts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // loop all data rows
                foreach (DataControlFieldCell cell in e.Row.Cells)
                {
                    // check all cells in one row
                    foreach (Control control in cell.Controls)
                    {
                        // Must use LinkButton here instead of ImageButton
                        // if you are having Links (not images) as the command button.
                        Button button = control as Button;
                        if (button != null && button.CommandName == "Delete")
                            // Add delete confirmation
                            button.OnClientClick = "if (!confirm('确定" +
                                   "要删除这个商品吗?')) return;";
                    }
                }
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            applyFilters();
        }
    }
}