using ProductUploader.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top.Api;
using Top.Api.Domain;
using Top.Api.Request;
using Top.Api.Response;
using Top.Api.Util;


namespace ProductUploader.Services
{
    /// <summary>
    /// 淘宝API产品相关Service
    /// </summary>
    public class TBProductUploadService
    {


        #region
        /// <summary>
        /// 初始化产品分类表 TB_ItemCat
        /// </summary>
        public void InitializationItemCat()
        {
            List<ItemCat> itemcatlist = GetItemCats();
            using (CatalogDataContext dct = new CatalogDataContext())
            {
                var allItemCat = dct.TB_ItemCats;
                List<ItemCat> itemcatinsert = new List<ItemCat>();
                //先判断数据库里是否存在，不存在的数据才需要插入
                if (allItemCat != null && allItemCat.Count() > 0)
                {
                    foreach (var itemcat in itemcatlist)
                    {
                        if (allItemCat.Where(i => i.Cid == itemcat.Cid && i.Name == itemcat.Name).FirstOrDefault() == null)
                        {
                            itemcatinsert.Add(itemcat);
                        }
                    }
                }
                else
                {
                    itemcatinsert.AddRange(itemcatlist);
                }
                //遍历获取到的淘宝分类数据插入TB_ItemCat
                foreach (var item in itemcatinsert)
                {
                    TB_ItemCat obj = new TB_ItemCat();
                    obj.ID = Guid.NewGuid();
                    obj.Cid = item.Cid;
                    obj.Name = item.Name;
                    obj.IsParent = item.IsParent;
                    if (string.IsNullOrEmpty(item.ModifiedTime) == false)
                    {
                        obj.ModifiedTime = DateTime.Parse(item.ModifiedTime);
                    }
                    obj.ModifiedType = item.ModifiedType;
                    obj.ParentCid = item.ParentCid;
                    obj.SortOrder = item.SortOrder;
                    obj.Status = item.Status;
                    dct.TB_ItemCats.InsertOnSubmit(obj);
                }
                dct.SubmitChanges();
            }

        }

        /// <summary>
        /// 初始化 属性表 TB_ItemProp
        /// </summary>
        public void InitializationItemProp()
        {

            using (CatalogDataContext dct = new CatalogDataContext())
            {
                //先清空TB_ItemProp表
                var allItemProp = dct.TB_ItemProps;
                List<ItemProp> itemproplist = new List<ItemProp>();
                var itemcatlist = dct.TB_ItemCats;
                List<ItemProp> itempropinsert = new List<ItemProp>();
                //获取ItemProp中所有字段
                string fieldsitemprop = @"child_template,cid,is_allow_alias,is_color_prop,is_enum_prop,is_input_prop,is_item_prop,is_key_prop,
            is_sale_prop,modified_time,modified_type,multi,must,name,parent_pid,parent_vid,pid,required,sort_order,status,type";
                foreach (var item in itemcatlist)
                {
                    if (item.IsParent == false)
                    {
                        //如果是叶子类目 调用API获取属性列表
                        List<ItemProp> tempitemprop = ProductService.GetItemprops(item.Cid, fieldsitemprop, null, null, null, null, null, null, null, null, null, 1, null);
                        foreach (var tempitem in tempitemprop)
                        {
                            tempitem.Cid = item.Cid;
                        }
                        itemproplist.AddRange(tempitemprop);
                    }
                }
                //先判断数据库里是否存在，不存在的数据才需要插入
                if (allItemProp != null && allItemProp.Count() > 0)
                {
                    foreach (var itemprop in itemproplist)
                    {
                        if (allItemProp.Where(i => i.Pid == itemprop.Pid && i.Cid == itemprop.Cid && i.Name == itemprop.Name).FirstOrDefault() == null)
                        {
                            itempropinsert.Add(itemprop);
                        }
                    }
                }
                else
                {
                    itempropinsert.AddRange(itemproplist);
                }
                if (itempropinsert != null && itempropinsert.Count > 0)
                {
                    //插入TB_ItemProp表
                    foreach (var item in itempropinsert)
                    {
                        TB_ItemProp obj = new TB_ItemProp();
                        obj.ID = Guid.NewGuid();
                        obj.Cid = item.Cid;
                        obj.Pid = item.Pid;
                        obj.ParentPid = item.ParentPid;
                        obj.ParentVid = item.ParentVid;
                        obj.ChildTemplate = item.ChildTemplate;
                        obj.IsAllowAlias = item.IsAllowAlias;
                        obj.IsColorProp = item.IsColorProp;
                        obj.IsEnumProp = item.IsEnumProp;
                        obj.IsInputProp = item.IsInputProp;
                        obj.IsItemProp = item.IsItemProp;
                        obj.IsKeyProp = item.IsKeyProp;
                        obj.IsSaleProp = item.IsSaleProp;
                        if (string.IsNullOrEmpty(item.ModifiedTime) == false)
                        {
                            obj.ModifiedTime = DateTime.Parse(item.ModifiedTime);
                        }
                        obj.ModifiedType = item.ModifiedType;
                        obj.Multi = item.Multi;
                        obj.Must = item.Must;
                        obj.Name = item.Name;
                        obj.Required = item.Required;
                        obj.SortOrder = item.SortOrder;
                        obj.Status = item.Status;
                        dct.TB_ItemProps.InsertOnSubmit(obj);
                    }
                }
                dct.SubmitChanges();
            }

        }

        /// <summary>
        /// 初始化 属性值表 TB_PropValue
        /// </summary>
        public void InitializationPropValue()
        {
            using (CatalogDataContext dct = new CatalogDataContext())
            {
                //清空TB_PropValue表
                var allPropValue = dct.TB_PropValues;
                List<PropValue> propvaluelist = new List<PropValue>();
                var itemcatlist = dct.TB_ItemCats;
                var itemproplist = dct.TB_ItemProps;
                List<PropValue> propvalueinsert = new List<PropValue>();
                //获取PropValue需要的字段.
                string fieldspropvalue = @"cid,pid,prop_name,vid,name,name_alias,status,sort_order";
                foreach (var item in itemcatlist)
                {
                    if (item.IsParent == false)
                    {
                        //如果是叶子类目 调用API获取属性列表
                        var tempitemprop = itemproplist.Where(i => i.Cid == item.Cid).ToList();
                        //如果是叶子类目，根据获取的属性列表
                        string pids = GetItemPropIDS(tempitemprop);
                        List<PropValue> tempprovalue = ProductService.GetItempropValues(fieldspropvalue, item.Cid, pids, 1, null);
                        propvaluelist.AddRange(tempprovalue);
                    }
                }
                //先判断数据库里是否存在，不存在的数据才需要插入
                if (allPropValue != null && allPropValue.Count() > 0)
                {
                    foreach (var propvalue in propvaluelist)
                    {
                        if (allPropValue.Where(i => i.Vid == propvalue.Vid && i.Pid == propvalue.Pid && i.Cid == propvalue.Cid && i.Name == propvalue.Name).FirstOrDefault() == null)
                        {
                            propvalueinsert.Add(propvalue);
                        }
                    }
                }
                else
                {
                    propvalueinsert.AddRange(propvaluelist);
                }
                if (propvalueinsert != null && propvalueinsert.Count > 0)
                {
                    //插入TB_PropValue表
                    int i = 0;
                    foreach (var item in propvaluelist)
                    {
                        i++;
                        TB_PropValue obj = new TB_PropValue();
                        obj.ID = Guid.NewGuid();
                        obj.Cid = item.Cid;
                        obj.IsParent = item.IsParent;
                        if (string.IsNullOrEmpty(item.ModifiedTime) == false)
                        {
                            obj.ModifiedTime = DateTime.Parse(item.ModifiedTime);
                        }
                        obj.ModifiedType = item.ModifiedType;
                        obj.Name = item.Name;
                        obj.NameAlias = item.NameAlias;
                        obj.Pid = item.Pid;
                        obj.PropName = item.PropName;
                        obj.SortOrder = item.SortOrder;
                        obj.Status = item.Status;
                        obj.Vid = item.Vid;
                        dct.TB_PropValues.InsertOnSubmit(obj);
                        if (i == 1000)
                        {
                            dct.SubmitChanges();
                            i = 0;
                        }
                    }
                }
            }
        }
        #endregion

        #region 初始化商品类目、属性、属性值 表数据

        //1.递归获取类目列表(is_parent==true的需要递归获取)
        //2.如果是叶子类目(is_parent==false)需要获取属性和属性值
        //3.注意：获取属性值的时候需要传入pid 否则获取不到


        /// <summary>
        /// 获取我们需要的类目(包括子类)
        /// </summary>
        /// <returns></returns>
        private List<ItemCat> GetItemCats()
        {
            //获取ItemCat中所有字段
            string fields = "cid,is_parent,modified_time,modified_type,name,parent_cid,sort_order,status";
            //暂时只获取（女装/女士精品：16 饰品/流行首饰/时尚饰品新：50013864 流行男鞋：50011740 男装：30 箱包皮具/热销女包/男包：50006842 服饰配件：50010404 女鞋：50006843）
            string cids = "16,50013864,50011740,30,50006842,50010404,50006843";
            //string cids = "50006843";
            //获取需要的根类目
            List<ItemCat> rootitemcats = ProductService.GetItemcats(fields, null, cids);
            List<ItemCat> subitemcats = new List<ItemCat>();
            foreach (var item in rootitemcats)
            {
                //遍历根类目，如果该类目是父类目，递归获取该类目下的所有子类目 存储在subitemcats中
                if (item.IsParent == true)
                {
                    GetItemCatsByParentCid(fields, item.Cid, ref subitemcats);
                }
            }
            //返回根类目和所有子类目
            if (subitemcats != null && subitemcats.Count > 0)
            {
                rootitemcats.AddRange(subitemcats);
            }
            return rootitemcats;
        }

        /// <summary>
        /// 遍历获取父类目下的所有子类目 子类目用ref参数返回
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="parentcid"></param>
        /// <param name="subitemcats"></param>
        private void GetItemCatsByParentCid(string fields, long? parentcid, ref List<ItemCat> subitemcats)
        {
            List<ItemCat> itemcatelist = ProductService.GetItemcats(fields, parentcid, null);
            subitemcats.AddRange(itemcatelist);
            foreach (var item in itemcatelist)
            {
                if (item.IsParent == true)
                {
                    //如果是父分类，再递归获取它下面的子分类
                    GetItemCatsByParentCid(fields, item.Cid, ref subitemcats);
                }
            }
        }

        /// <summary>
        /// 根据传入的属性列表 返回Pid1;Pid2的形式
        /// </summary>
        /// <param name="itemproplist"></param>
        /// <returns></returns>
        private string GetItemPropIDS(List<TB_ItemProp> itemproplist)
        {
            string stritempropids = "";
            StringBuilder sbitempropids = new StringBuilder();
            foreach (var item in itemproplist)
            {
                sbitempropids.Append(item.Pid + ";");
            }
            if (sbitempropids != null)
            {
                stritempropids = sbitempropids.ToString();
                stritempropids.TrimEnd(';');
            }
            return stritempropids;
        }
        #endregion

        public void ConvertSSProductToTaoBao(long ProductID)
        {
            using (CatalogDataContext dct = new CatalogDataContext())
            {
                var p = dct.SS_Products.Single(o => o.product_id == ProductID);
                List<TB_Product> tbproductlist = new List<TB_Product>();                                
                TBProductAdapterService tbproductadapater = new TBProductAdapterService();

                var tbproduct = tbproductadapater.ProductConvert(p);
                dct.TB_Products.InsertOnSubmit(tbproduct);
                dct.SubmitChanges();
            }

        }

        //public void ConvertSSProductToTaoBao(string productSetName)
        //{
        //    if (!String.IsNullOrEmpty(productSetName))
        //    {
        //        using (CatalogDataContext dct = new CatalogDataContext())
        //        {

        //            //获取ssCat_ID=puffer-coats的数据存入tb数据库
        //            var SS = dct.SS_Categories;
        //            //List<SS_Product> ssproductlist = _SSCategoryRepository.Find(Specification<SS_Category>.Eval(o => o.Cat_ID == "puffer-coats")).ProductCollection.ToList();
        //            var ssProductList = from product in dct.SS_Products where product.SS_Product_Set.product_set_name == productSetName select product;
        //            List<TB_Product> tbproductlist = new List<TB_Product>();
        //            TBProductAdapterService tbproductadapater = new TBProductAdapterService();
        //            foreach (var item in ssProductList)
        //            {
        //                var tbproduct = tbproductadapater.ProductConvert(item);
        //                dct.TB_Products.InsertOnSubmit(tbproduct);
        //            }
        //            dct.SubmitChanges();
        //        }
        //    }
        //    else
        //        throw new ArgumentNullException("商品组名称不能为空");
           
        //}

        public void UploadProduct(string productSetName)
        {
            try
            {
                if (!string.IsNullOrEmpty(productSetName))
                {
                    using (CatalogDataContext dct = new CatalogDataContext())
                    {
                        var tbProductList = from tbProduct in dct.TB_Products where tbProduct.SS_Product.SS_Product_Set.product_set_name == productSetName && tbProduct.IsUploaded == false select tbProduct;
                        foreach (var item in tbProductList)
                        {
                            try
                            {
                                Item product = ProductService.AddItem(item);

                                dct.SubmitChanges();

                                if (!String.IsNullOrEmpty(item.SkuProperties))
                                {
                                    string[] skus = item.SkuProperties.Split(new char[] { ',' });

                                    foreach (var sku in skus)
                                    {
                                        string colorPV = getColorProperties(sku);

                                        if (!string.IsNullOrEmpty(colorPV))
                                        {
                                            string[] cVid = colorPV.Split(new char[] { ':' });

                                            var ssPicUrl = (from color in dct.SS_Product_Color_Image_Mappings join ssImage in dct.SS_Images on color.image_id equals ssImage.image_id join tbColor in dct.Mapping_Colors on color.color_name equals tbColor.ss_color where color.product_id == item.SSProductID && ssImage.size_name == "Original" && tbColor.tb_vid == long.Parse(cVid[1]) select new { url = ssImage.url }).FirstOrDefault().url;
                                            string ImgFilePath = "";
                                            Utils.SaveImage(ssPicUrl, out ImgFilePath);
                                            ProductService.UploadItemPropImg(product.NumIid, colorPV, ConfigurationManager.AppSettings["ImageFolderPath"] + ImgFilePath);
                                        }
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                
                                continue;
                            }


                          
                        }
                    }
                }
                else
                    throw new ArgumentNullException("商品组名称不能为空");
                
            }
            catch
            {
                throw;
            }
        }

        private string getColorProperties(string properties)
        {
            string[] pv = properties.Split(new char[] { ';' });

            foreach (var i in pv)
            {
                if (i.Contains("1627207") || i.Contains("20510") || i.Contains("1627777") || i.Contains("122276380"))
                {
                    return i;
                }
            }
            return null;
        }
    }
}
