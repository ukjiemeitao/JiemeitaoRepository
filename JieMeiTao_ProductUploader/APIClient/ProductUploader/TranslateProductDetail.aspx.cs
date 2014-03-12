using ProductUploader.DAL;
using ProductUploader.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProductUploader
{
    public partial class TranslateProductDetail : BasePage
    {
        long productid ;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(Request["product_id"]))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "aa", "alert('product id is null')", true);
                btnSubmit.Enabled = false;
            }
            else
            {
                long.TryParse(Request["product_id"], out productid);
            }
            if (!this.IsPostBack)
            {
                              
                BindData();
                btnSubmit.Enabled = true;
                
            }
        }

        protected void BindData()
        {
            using (CatalogDataContext dct = new CatalogDataContext())
            {

                #region Populate two DropDownLists

                ddlTitlePrefixs.DataSource = dct.TB_Title_Prefixes;
                ddlTitlePrefixs.DataTextField = "title_prefix";
                ddlTitlePrefixs.DataValueField = "id";
                ddlTitlePrefixs.DataBind();
                ddlTitlePrefixs.Items.Insert(0, new ListItem("", "NA"));

                ddlModels.DataSource = dct.TB_Models;
                ddlModels.DataTextField = "model_name";
                ddlModels.DataValueField = "id";
                ddlModels.DataBind();
                ddlModels.Items.Insert(0, new ListItem("", "NA"));

                #endregion


                #region Populate SS_Product
                var product = dct.SS_Products.SingleOrDefault(o => o.product_id == productid);                

                if (product != null)
                {
                    lbID.Text = product.product_id.ToString();
                    lbTitle.Text = product.name;
                    lbDescription.Text = product.description;
                    tbTitle.Text = product.chinese_name; ////TODO:建立起名短语
                    lbUrl.NavigateUrl = product.click_url;
                    FCKeditor1.Value = product.chinese_description;

                    btnSubmit.Enabled = true;
                }
                else
                {
                    btnSubmit.Enabled = false;
                }
                #endregion                
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbTitle.Text.Length <= 30)
                {
                    using (CatalogDataContext dct = new CatalogDataContext())
                    {
                        var p = dct.SS_Products.SingleOrDefault(o => o.product_id == productid);
                       

                        if (p != null)
                        {
                            p.chinese_name = tbTitle.Text;
                            p.chinese_description = FCKeditor1.Value;
                            p.istranslated = true;
                            dct.SubmitChanges();

                            var tbp = dct.TB_Products.SingleOrDefault(t => t.SSProductID == productid);

                            if (tbp == null)
                            {
                                
                                TBProductUploadService toTaobao = new TBProductUploadService();
                                toTaobao.ConvertSSProductToTaoBao(productid);
                            }
                            else
                            {
                                tbp.Title = tbTitle.Text;
                                tbp.Desc = FCKeditor1.Value;
                                dct.SubmitChanges();
                            }
                          

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "aa", "alert('提交成功');", true);
                        }
                        
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Close", "window.close()", true);


                    }
                }
                else
                    throw new ApplicationException("商品题目超过30个字节");
            }
            catch (Exception ex)
            {

                string message = ex.Message.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + message + "')", true);
            }                      
        }

        protected void ddlTitlePrefixs_SelectedIndexChanged(object sender, EventArgs e)
        {

            tbTitle.Text = ddlTitlePrefixs.SelectedItem.Text + tbTitle.Text;
            
        }

        protected void ddlModels_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (CatalogDataContext dct = new CatalogDataContext())
            {
                var model = dct.TB_Models.SingleOrDefault(o => o.model_name == "拍前必读");

                FCKeditor1.Value = FCKeditor1.Value + model.model_html;
            }
           
        }
    }
}