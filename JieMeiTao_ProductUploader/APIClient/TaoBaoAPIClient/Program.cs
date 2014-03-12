using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top.Api.Domain;

namespace TaoBaoAPIClient
{
    class Program
    {
        static void Main(string[] args)
        {
            DownloadTBCategories();
            DownloadTBProps();
            InitializationPropValue();
        }

        private static void DownloadTBCategories()
        {

            TBProductUploadServiceImpl impl = new TBProductUploadServiceImpl();
            List<ItemCat> catList = impl.GetItemCats();


            foreach (var cat in catList)
            {
                using (TaoBaoDataContext tbCon = new TaoBaoDataContext())
                {
                    TB_ItemCat obj = new TB_ItemCat();
                    obj.ID = Guid.NewGuid();
                    obj.Cid = cat.Cid;
                    obj.Name = cat.Name;
                    obj.IsParent = cat.IsParent;

                    if (!string.IsNullOrEmpty(cat.ModifiedTime))
                    {
                        obj.ModifiedTime = DateTime.Parse(cat.ModifiedTime);
                    }
                    obj.ModifiedType = cat.ModifiedType;
                    obj.ParentCid = cat.ParentCid;
                    obj.SortOrder = (int)cat.SortOrder;
                    obj.Status = cat.Status;

                    tbCon.TB_ItemCats.InsertOnSubmit(obj);
                    tbCon.SubmitChanges();
                }


            }
        }

        private static void DownloadTBProps()
        {
            using (TaoBaoDataContext tbCon = new TaoBaoDataContext())
            {
                //先清空TB_ItemProp表
                var allItemProp = tbCon.TB_ItemProps.ToList<TB_ItemProp>();
                List<ItemProp> itemproplist = new List<ItemProp>();
                var itemcatlist = tbCon.TB_ItemCats.ToList<TB_ItemCat>();
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
                        tbCon.TB_ItemProps.InsertOnSubmit(obj);
                    }
                }
                tbCon.SubmitChanges();

            }

        }

        private static void InitializationPropValue()
        {
            using (TaoBaoDataContext dct = new TaoBaoDataContext())
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

        #region 实现wcf接口方法
        /// <summary>
        /// 初始化产品分类表 TB_ItemCat
        /// </summary>
        //public void InitializationItemCat()
        //{
        //    List<ItemCat> itemcatlist = GetItemCats();
        //    //先清空ItemCat表
        //    var allItemCat = _TBItemCatRepository.FindAll();
        //    foreach (var cat in allItemCat)
        //    {
        //        _TBItemCatRepository.Remove(cat);
        //    }
        //    //遍历获取到的淘宝分类数据插入TB_ItemCat
        //    foreach (var item in itemcatlist)
        //    {
        //        TB_ItemCat obj = new TB_ItemCat();
        //        obj.ID = Guid.NewGuid();
        //        obj.Cid = item.Cid;
        //        obj.Name = item.Name;
        //        obj.IsParent = item.IsParent;
        //        if (string.IsNullOrEmpty(item.ModifiedTime) == false)
        //        {
        //            obj.ModifiedTime = DateTime.Parse(item.ModifiedTime);
        //        }
        //        obj.ModifiedType = item.ModifiedType;
        //        obj.ParentCid = item.ParentCid;
        //        obj.SortOrder = (int)item.SortOrder;
        //        obj.Status = item.Status;
        //        _TBItemCatRepository.Add(obj);
        //    }
        //    Context.Commit();
        //}

        /// <summary>
        /// 初始化 属性表 TB_ItemProp
        /// </summary>
        //        public void InitializationItemProp()
        //        {
        //            //先清空TB_ItemProp表
        //            var allItemProp = _TBItemPropRepository.FindAll();
        //            foreach (var item in allItemProp)
        //            {
        //                _TBItemPropRepository.Remove(item);
        //            }
        //            List<ItemProp> itemproplist = new List<ItemProp>();
        //            var itemcatlist = _TBItemCatRepository.FindAll();
        //            //获取ItemProp中所有字段
        //            string fieldsitemprop = @"child_template,cid,is_allow_alias,is_color_prop,is_enum_prop,is_input_prop,is_item_prop,is_key_prop,
        //            is_sale_prop,modified_time,modified_type,multi,must,name,parent_pid,parent_vid,pid,required,sort_order,status,type";
        //            foreach (var item in itemcatlist)
        //            {
        //                if (item.IsParent == false)
        //                {
        //                    //如果是叶子类目 调用API获取属性列表
        //                    List<ItemProp> tempitemprop = ProductService.GetItemprops(item.Cid, fieldsitemprop, null, null, null, null, null, null, null, null, null, 1, null);
        //                    foreach (var tempitem in tempitemprop)
        //                    {
        //                        tempitem.Cid = item.Cid.Value;
        //                    }
        //                    itemproplist.AddRange(tempitemprop);
        //                }
        //            }
        //            if (itemproplist != null && itemproplist.Count > 0)
        //            {
        //                //插入TB_ItemProp表
        //                foreach (var item in itemproplist)
        //                {
        //                    TB_ItemProp obj = new TB_ItemProp();
        //                    obj.ID = Guid.NewGuid();
        //                    obj.Cid = item.Cid;
        //                    obj.Pid = item.Pid;
        //                    obj.ParentPid = item.ParentPid;
        //                    obj.ParentVid = item.ParentVid;
        //                    obj.ChildTemplate = item.ChildTemplate;
        //                    obj.IsAllowAlias = item.IsAllowAlias;
        //                    obj.IsColorProp = item.IsColorProp;
        //                    obj.IsEnumProp = item.IsEnumProp;
        //                    obj.IsInputProp = item.IsInputProp;
        //                    obj.IsItemProp = item.IsItemProp;
        //                    obj.IsKeyProp = item.IsKeyProp;
        //                    obj.IsSaleProp = item.IsSaleProp;
        //                    if (string.IsNullOrEmpty(item.ModifiedTime) == false)
        //                    {
        //                        obj.ModifiedTime = DateTime.Parse(item.ModifiedTime);
        //                    }
        //                    obj.ModifiedType = item.ModifiedType;
        //                    obj.Multi = item.Multi;
        //                    obj.Must = item.Must;
        //                    obj.Name = item.Name;
        //                    obj.Required = item.Required;
        //                    int orderid = 0;
        //                    if (int.TryParse(item.SortOrder.ToString(), out orderid))
        //                    {
        //                        obj.SortOrder = orderid;
        //                    }
        //                    obj.Status = item.Status;
        //                    _TBItemPropRepository.Add(obj);
        //                }
        //            }
        //            Context.Commit();
        //        }

        /// <summary>
        /// 初始化 属性值表 TB_PropValue
        /// </summary>
        //public void InitializationPropValue()
        //{
        //    //清空TB_PropValue表
        //    var allPropValue = _TBPropValueRepository.FindAll();
        //    foreach (var item in allPropValue)
        //    {
        //        _TBPropValueRepository.Remove(item);
        //    }
        //    List<PropValue> propvaluelist = new List<PropValue>();
        //    var itemcatlist = _TBItemCatRepository.FindAll();
        //    var itemproplist = _TBItemPropRepository.FindAll();
        //    //获取PropValue需要的字段.
        //    string fieldspropvalue = @"cid,pid,prop_name,vid,name,name_alias,status,sort_order";
        //    foreach (var item in itemcatlist)
        //    {
        //        if (item.IsParent == false)
        //        {
        //            //如果是叶子类目 调用API获取属性列表
        //            var tempitemprop = itemproplist.Where(i => i.Cid == item.Cid).ToList();
        //            //如果是叶子类目，根据获取的属性列表
        //            string pids = GetItemPropIDS(tempitemprop);
        //            List<PropValue> tempprovalue = ProductService.GetItempropValues(fieldspropvalue, item.Cid, pids, 1, null);
        //            propvaluelist.AddRange(tempprovalue);
        //        }
        //    }
        //    if (propvaluelist != null && propvaluelist.Count > 0)
        //    {
        //        //插入TB_PropValue表
        //        int i = 0;
        //        foreach (var item in propvaluelist)
        //        {
        //            i++;
        //            TB_PropValue obj = new TB_PropValue();
        //            obj.ID = Guid.NewGuid();
        //            obj.Cid = item.Cid;
        //            obj.IsParent = item.IsParent;
        //            if (string.IsNullOrEmpty(item.ModifiedTime) == false)
        //            {
        //                obj.ModifiedTime = DateTime.Parse(item.ModifiedTime);
        //            }
        //            obj.ModifiedType = item.ModifiedType;
        //            obj.Name = item.Name;
        //            obj.NameAlias = item.NameAlias;
        //            obj.Pid = item.Pid;
        //            obj.PropName = item.PropName;
        //            int orderid = 0;
        //            if (int.TryParse(item.SortOrder.ToString(), out orderid))
        //            {
        //                obj.SortOrder = orderid;
        //            }
        //            obj.Status = item.Status;
        //            obj.Vid = item.Vid;
        //            _TBPropValueRepository.Add(obj);
        //            if (i == 100)
        //            {
        //                Context.Commit();
        //                i = 0;
        //            }
        //        }
        //    }

        //}
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
        private static void GetItemCatsByParentCid(string fields, long? parentcid, ref List<ItemCat> subitemcats)
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
        private static string GetItemPropIDS(List<TB_ItemProp> itemproplist)
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
    }
}
