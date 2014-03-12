using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.Adapters;
using ProductUploader.DAL;
using System.Text;
using ProductUploader.Services;


namespace ProductUploader
{
    public partial class PropertiesSetting : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                CatalogDataContext dct = new CatalogDataContext();
                

                if (!this.IsPostBack)
                {
                    
                    //获取所有SS_Product的分类
                    var ssCats = (from mapping in dct.SS_Product_Category_Mappings select new { Name = mapping.SS_Category.name, CatID = mapping.cat_id}).Distinct();
                    if (ssCats != null)
                    {
                        ddlSSCats.DataSource = ssCats;
                        ddlSSCats.DataTextField = "CatID";
                        ddlSSCats.DataValueField = "CatID";
                        ddlSSCats.DataBind();
                    }
                    
                    //获取淘宝分类列表
                    var tbParentCats = from cat in dct.TB_ItemCats where new[] { 16L, 50013864L, 50011740L, 30L, 50006842L, 50010404L, 50006843L }.Contains(cat.Cid) select cat;
                    tvCats.Nodes.Clear();


                    foreach (var cat in tbParentCats)
                    {
                        TreeNode node = new TreeNode(cat.Name, cat.Cid.ToString());
                        tvCats.Nodes.Add(node);
                    }

                    foreach (TreeNode node in tvCats.Nodes)
                    {
                        populateNode(node);
                    }

                    if (ddlSSCats.SelectedItem != null) 
                    {
                        string ssCid = ddlSSCats.SelectedItem.Value;
                        findAndHightLightNode(ssCid);
                    }                    
                }

                if ( this.IsPostBack && !string.IsNullOrEmpty(tvCats.SelectedValue) && (string.IsNullOrEmpty(this.GetPostBackControlId()) || this.GetPostBackControlId() == "btnSaveCat"))
                    populateProps(long.Parse(tvCats.SelectedValue));
                
            }
            catch (Exception ex)
            {
                string message = ex.Message.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + message + "')", true);
            }

           
           
        }

       

        private void findAndHightLightNode(string ssCid)
        {
            if (!String.IsNullOrEmpty(ssCid))
            {
                using (CatalogDataContext dct = new CatalogDataContext())
                {
                    var matchedTBCat = (from mapping in dct.Mapping_Categories where mapping.ss_cat_id == ssCid select mapping.tb_cid).FirstOrDefault();

                    if (matchedTBCat != null)
                    {
                        string valuePath = getTreeViewValuePath(matchedTBCat.Value);

                        highLightNode(valuePath.Substring(2, valuePath.Length - 2));
                    }
                    else
                    {
                        if (tvCats.SelectedNode != null)
                        {
                            tvCats.SelectedNode.Parent.Expanded = false;
                            tvCats.SelectedNode.Selected = false;
                            
                        }
                           
                        tvCats.ExpandDepth = 0;
                        
                    }
                       

                }
            }
            else
                throw new ArgumentNullException("SS商品类ID不能为空");
        }

        private string getTreeViewValuePath(long tbCid)
        {
            StringBuilder valuePath = new StringBuilder();
            valuePath.Append(tvCats.PathSeparator + tbCid.ToString());

            using (CatalogDataContext dct = new CatalogDataContext())
            {
                var parentCat = (from cat in dct.TB_ItemCats where cat.Cid == tbCid && cat.ParentCid != 0 select cat.ParentCid).FirstOrDefault();
                if (parentCat != null)
                    valuePath.Insert(0, tvCats.PathSeparator + getTreeViewValuePath(parentCat.Value));
                
            }

            return valuePath.ToString();
            
        }

        private void highLightNode(string valuePath)
        {
            TreeNode node = tvCats.FindNode(valuePath);
            if (node != null)
            {
                node.Parent.Expand();
                node.Selected = true;
                node.SelectAction = TreeNodeSelectAction.Select;
                
                populateProps(long.Parse(node.Value));
            }            
        }

        private void populateNode(TreeNode node)
        {
            using (CatalogDataContext dct = new CatalogDataContext())
            {
                var cats = from cat in dct.TB_ItemCats where cat.ParentCid == long.Parse(node.Value) select cat;
                foreach (var item in cats)
                {
                    TreeNode n = new TreeNode(item.Name, item.Cid.ToString());
                    node.ChildNodes.Add(n);
                }
                foreach (TreeNode item in node.ChildNodes)
                {
                    populateNode(item);
                }
            }           
        }

        private void populateProps(long tbCatId)
        {
            using (CatalogDataContext dct = new CatalogDataContext())
            {
                long tbCid = long.Parse(tvCats.SelectedValue);

                #region 显示销售属性匹配区

                var saleProps = from prop in dct.TB_ItemProps where prop.Cid == tbCid && prop.IsSaleProp == true select prop;

                HtmlGenericControl saleDiv = new HtmlGenericControl("div");
                saleDiv.ID = "SalePropsSection";
                saleDiv.Attributes.Add("class", "dynamicdiv");
                

                foreach (var prop in saleProps)
                {
                    //找到该淘宝分类下的颜色属性，匹配没有匹配过的shopstyle颜色和该分类下的颜色属性值，插入新的匹配到Mapping_Colors表
                    if (prop.Name.Contains("颜色"))
                    {
                        HtmlGenericControl divColor = new HtmlGenericControl("div");
                        divColor.ID = "color";
                        divColor.Attributes.Add("class", "dynamicdiv");

                        var newColors = (from color in dct.SS_Product_Color_Image_Mappings.Where(c => (c.SS_Product.SS_Product_Category_Mappings.Where(cat => cat.cat_id == ddlSSCats.SelectedValue).Single().cat_id == ddlSSCats.SelectedValue)) where !dct.Mapping_Colors.Any(m => m.ss_color == color.color_name && m.tb_cid == prop.Cid) select new { ColorName = color.color_name }).Distinct();

                        int counter = newColors.Count();
                        ViewState["NewColorsCouter"] = counter;

                        for (int i = 0; i < counter;i++)
                        {
                            HtmlGenericControl div = new HtmlGenericControl("div");
                            div.ID = "color" + i;
                            div.Attributes.Add("class", "dynamicdiv");

                            Table t = new Table();
                            TableRow r = new TableRow();
                            TableCell c1 = new TableCell();
                            c1.Attributes.Add("width", "200px");
                            TableCell c2 = new TableCell();


                            Label lbl = new Label();
                            lbl.ID = "lblColor" + i;
                            lbl.Text = newColors.ToArray()[i].ColorName;
                            c1.Controls.Add(lbl);

                            DropDownList ddl = new DropDownList();
                            ddl.ID = "ddlColor" + i;
                            ddl.DataSource = from color in dct.TB_PropValues where color.Cid == tbCid && color.Pid == prop.Pid select new { Name = color.Name, Vid = color.Vid };
                            ddl.DataTextField = "Name";
                            ddl.DataValueField = "Vid";
                            ddl.DataBind();
                            c2.Controls.Add(ddl);

                            c1.Controls.Add(lbl);
                            c2.Controls.Add(ddl);
                            r.Cells.Add(c1);
                            r.Cells.Add(c2);
                            t.Rows.Add(r);

                            div.Controls.Add(t);
                            divColor.Controls.Add(div);

                        }


                        Button btnSubmit = new Button() { Text = "添加颜色匹配" };
                        btnSubmit.ID = "btnSubmit";
                        btnSubmit.Click += btnColorSubmit_Click;

                        if (counter == 0)
                        {
                            HtmlGenericControl div = new HtmlGenericControl("div");
                            div.ID = "divNoNewColor";
                            div.InnerText = "没有需要匹配的SS颜色";                            
                            divColor.Controls.Add(div);
                        }
                        else
                        {
                            divColor.Controls.Add(btnSubmit);
                        }

                        saleDiv.Controls.Add(divColor);
                       
                    } //找到该淘宝分类下的尺码属性，匹配没有匹配过的shopstyle尺码和该分类下的尺码属性值，插入新的匹配到Mapping_Sizes表
                    else if (prop.Name.Contains("尺寸") || prop.Name.Contains("尺码") || prop.Name.Contains("鞋尺码") || prop.Name.Contains("帽围尺码") || prop.Name.Contains("周长") || prop.Name.Contains("包袋大小"))
                    {
                        //var newSizes = (from size in dct.SS_Product_Size_Mappings.Where(m => m.SS_Product.SS_Product_Category_Mappings.Where(c => c.cat_id == ddlSSCats.SelectedValue).Single().cat_id == ddlSSCats.SelectedValue) where !dct.Mapping_Sizes.Any(ms => ms.ss_size_name == size.SS_Size.name && ms.tb_cid == tbCid) select new { SizeName = size.SS_Size.name }).Distinct();

                        var newSizes = (from size in dct.SS_Product_Size_Mappings.Where(m => m.SS_Product.SS_Product_Category_Mappings.Where(c => c.cat_id == ddlSSCats.SelectedValue).Single().cat_id == ddlSSCats.SelectedValue) where !dct.Mapping_Sizes.Any(ms => ms.ss_size_name == size.SS_Size.name && ms.tb_cid == tbCid) select new { SizeName = size.SS_Size.name }).Distinct();

                        HtmlGenericControl divSize = new HtmlGenericControl("div");
                        divSize.ID = "size";
                        divSize.Attributes.Add("class", "dynamicdiv");

                        int counter = newSizes.Count();
                        ViewState["NewSizesCounter"] = counter;

                        for (int i = 0; i < counter; i++)
                        {
                            HtmlGenericControl div = new HtmlGenericControl("div");
                            div.ID = "size" + i;
                            div.Attributes.Add("class", "dynamicdiv");

                            Table t = new Table();
                            TableRow r = new TableRow();
                            TableCell c1 = new TableCell();
                            c1.Attributes.Add("width", "200px");
                            TableCell c2 = new TableCell();


                            Label lbl = new Label();
                            lbl.ID = "lblSize" + i;
                            lbl.Text = newSizes.ToArray()[i].SizeName;
                            c1.Controls.Add(lbl);

                            DropDownList ddl = new DropDownList();
                            ddl.ID = "ddlSize" + i;
                            ddl.DataSource = from size in dct.TB_PropValues where size.Pid == prop.Pid && size.Cid == tbCid select new { Name = size.Name, Vid = size.Vid };
                            ddl.DataTextField = "Name";
                            ddl.DataValueField = "Vid";
                            ddl.DataBind();
                            c2.Controls.Add(ddl);

                            c1.Controls.Add(lbl);
                            c2.Controls.Add(ddl);
                            r.Cells.Add(c1);
                            r.Cells.Add(c2);
                            t.Rows.Add(r);

                            div.Controls.Add(t);
                            divSize.Controls.Add(div);
                        }

                        Button btnSubmit = new Button() { Text = "添加尺码匹配" };
                        btnSubmit.ID = "btnSizeSubmit";                       
                        btnSubmit.Click += btnSizeSubmit_Click;

                        if (counter == 0)
                        {
                            HtmlGenericControl div = new HtmlGenericControl("div");
                            div.ID = "divNoNewSize";
                            div.InnerText = "没有需要匹配的SS尺码";
                            divSize.Controls.Add(div);
                        }
                        else
                        {
                            divSize.Controls.Add(btnSubmit);
                        }

                        saleDiv.Controls.Add(divSize);                       
                    }
                    phPropsMapping.Controls.Add(saleDiv);
                }
                #endregion

                #region 显示必须属性
                var mustProps = from p in (from prop in dct.TB_ItemProps where prop.Cid == tbCid && prop.Must == true select prop) where !dct.Mapping_Categories_Props.Any(m => m.Mapping_Category.tb_cid == p.Cid && m.pid == p.Pid) select p;

                HtmlGenericControl mustDiv = new HtmlGenericControl("div");
                mustDiv.ID = "MustPropsSection";
                mustDiv.Attributes.Add("class", "dynamicdiv");

                int mustPropsCounter =  mustProps.Count();
                ViewState["MustPropsCounter"] = mustPropsCounter;

                for (int i = 0; i < mustPropsCounter; i++)
                {
                    if (mustProps.ToArray()[i].Name.Contains("尺码") || mustProps.ToArray()[i].Name.Contains("鞋尺码"))
                        continue;

                    HtmlGenericControl div = new HtmlGenericControl("div");
                    div.ID = "must" + i;
                    div.Attributes.Add("class", "dynamicdiv");

                    Table t = new Table();
                    TableRow r = new TableRow();
                    TableCell c1 = new TableCell();
                    c1.Attributes.Add("width", "200px");
                    TableCell c2 = new TableCell();


                    Label lbl = new Label();
                    lbl.ID = "lblMust" + i;
                    lbl.Text = mustProps.ToArray()[i].Name;                    
                    c1.Controls.Add(lbl);

                    DropDownList ddl = new DropDownList();
                    ddl.ID = "ddlMust" + i;
                    ddl.DataSource = from value in dct.TB_PropValues where value.Pid == mustProps.ToArray()[i].Pid && value.Cid == tbCatId select new { Name = value.Name, Vid = value.Vid };
                    ddl.DataTextField = "Name";
                    ddl.DataValueField = "Vid";
                    ddl.DataBind();
                    c2.Controls.Add(ddl);

                    c1.Controls.Add(lbl);
                    c2.Controls.Add(ddl);
                    r.Cells.Add(c1);
                    r.Cells.Add(c2);
                    t.Rows.Add(r);

                    div.Controls.Add(t);
                    mustDiv.Controls.Add(div);
                    
                }


                Button btnMustSubmit = new Button() { Text = "添加必须属性匹配" };
                btnMustSubmit.ID = "btnMustSubmit";
                btnMustSubmit.Click += btnMustSubmit_Click;

                if (mustPropsCounter == 0)
                {
                    HtmlGenericControl div = new HtmlGenericControl("div");
                    div.ID = "divNoMustProp";
                    div.InnerText = "没有需要匹配的必须属性";
                    mustDiv.Controls.Add(div);
                }
                else
                {
                    mustDiv.Controls.Add(btnMustSubmit);
                }


                phPropsMapping.Controls.Add(mustDiv);

                #endregion
               
            }
        }

        protected void btnColorSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["NewColorsCouter"] != null && tvCats.SelectedValue != null)
                {
                    using (CatalogDataContext dct = new CatalogDataContext())
                    {
                        int counter = int.Parse(ViewState["NewColorsCouter"].ToString());

                        for (int i = 0; i < counter; i++)
                        {
                            Label lbl = (Label)this.Master.FindControl("MainContent").FindControl("lblColor" + i);
                            DropDownList ddl = (DropDownList)this.Master.FindControl("MainContent").FindControl("ddlColor" + i);

                            Mapping_Color mColor = new Mapping_Color() { id = System.Guid.NewGuid(), ss_color = lbl.Text, tb_color = ddl.SelectedItem.Text, tb_vid = long.Parse(ddl.SelectedItem.Value), tb_cid = long.Parse(tvCats.SelectedValue) };

                            dct.Mapping_Colors.InsertOnSubmit(mColor);
                        }

                        dct.SubmitChanges();
                    }
                }
                else
                    throw new ArgumentNullException("ViewState中没有NewColorsCounter"); 
            }
            catch (Exception ex)
            {

                string message = ex.Message.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + message + "')", true);
            }
                         
                                                    
        }

        protected void btnSizeSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["NewSizesCounter"] != null && tvCats.SelectedNode != null)
                {
                    using (CatalogDataContext dct = new CatalogDataContext())
                    {
                        int counter = int.Parse(ViewState["NewSizesCounter"].ToString());

                        for (int i = 0; i < counter; i++)
                        {
                            Label lbl = (Label)this.Master.FindControl("MainContent").FindControl("lblSize" + i);
                            DropDownList ddl = (DropDownList)this.Master.FindControl("MainContent").FindControl("ddlSize" + i);
                            Mapping_Size mSize = new Mapping_Size() { id = System.Guid.NewGuid(), ss_size_name = lbl.Text, tb_size_name = ddl.SelectedItem.Text, tb_vid = long.Parse(ddl.SelectedValue), tb_cid = long.Parse(tvCats.SelectedValue) };

                            dct.Mapping_Sizes.InsertOnSubmit(mSize);
                        }

                        dct.SubmitChanges();
                    }
                }
                else
                    throw new ArgumentNullException("ViewState中没有NewSizesCounter,或者ViewState中没有TBCid");
            }
            catch (Exception ex)
            {

                string message = ex.Message.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + message + "')", true);
            }                        
        }

        protected void btnMustSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["MustPropsCounter"] != null && tvCats.SelectedNode != null)
                {
                    using (CatalogDataContext dct = new CatalogDataContext())
                    {
                        var mCat = from mapping in dct.Mapping_Categories where mapping.tb_cid == long.Parse(tvCats.SelectedValue) && mapping.ss_cat_id == ddlSSCats.SelectedValue select mapping;

                        if (mCat.Single() != null)
                        {
                            int counter = int.Parse(ViewState["MustPropsCounter"].ToString());

                            for (int i = 0; i < counter; i++)
                            {
                                Label lbl = (Label)this.Master.FindControl("MainContent").FindControl("lblMust" + i);
                                DropDownList ddl = (DropDownList)this.Master.FindControl("MainContent").FindControl("ddlMust" + i);

                                var prop = (from pv in dct.TB_ItemProps where pv.Cid == long.Parse(tvCats.SelectedValue) && pv.Name == lbl.Text select pv).Single();

                                Mapping_Categories_Prop mMust = new Mapping_Categories_Prop() { id = System.Guid.NewGuid(), mapping_categories_id = mCat.Single().id, pid = prop.Pid, vid = long.Parse(ddl.SelectedValue) };

                                dct.Mapping_Categories_Props.InsertOnSubmit(mMust);

                                dct.SubmitChanges();


                            }
                        }
                        else
                            throw new ArgumentException("还未匹配ss分类，淘宝分类为空");
                        

                    }
                }
            }
            catch (Exception ex)
            {

                string message = ex.Message.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + message + "')", true);
            }
        }



        protected void ddlSSCats_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string ssCid = ddlSSCats.SelectedItem.Value;

                findAndHightLightNode(ssCid);
            }
            catch (Exception ex)
            {

                string message = ex.Message.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + message + "')", true);
            }

        }

        protected void tvCats_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {                
                populateProps(long.Parse(tvCats.SelectedValue));
            }
            catch (Exception ex)
            {
                string message = ex.Message.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + message + "')", true);
            }
           
        }

        protected void btnSaveCat_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvCats.SelectedNode != null)
                {
                    using (CatalogDataContext dct = new CatalogDataContext())
                    {
                        Mapping_Category mc = new Mapping_Category() { id = System.Guid.NewGuid(), ss_cat_id = ddlSSCats.SelectedValue, ss_cat_name = ddlSSCats.SelectedItem.Text, tb_cid = long.Parse(tvCats.SelectedValue), tb_name = tvCats.SelectedNode.Text };
                        dct.Mapping_Categories.InsertOnSubmit(mc);
                        dct.SubmitChanges();
                    }


                }
                else
                    throw new ApplicationException("请选择淘宝分类");
            }
            catch (Exception ex)
            {

                string message = ex.Message.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + message + "')", true);
            }
        }
    }
}