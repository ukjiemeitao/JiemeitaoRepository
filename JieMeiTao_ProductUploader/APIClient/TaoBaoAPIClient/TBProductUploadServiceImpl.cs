using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Top.Api.Domain;



namespace TaoBaoAPIClient
{
    /// <summary>
    /// 淘宝上传商品用到的Service
    /// </summary>
    public class TBProductUploadServiceImpl
    {
      

        #region 实现wcf接口方法
        /// <summary>
        /// 初始化产品分类表 TB_ItemCat
        /// </summary>
      

        #region 初始化商品类目、属性、属性值 表数据

        //1.递归获取类目列表(is_parent==true的需要递归获取)
        //2.如果是叶子类目(is_parent==false)需要获取属性和属性值
        //3.注意：获取属性值的时候需要传入pid 否则获取不到


        /// <summary>
        /// 获取我们需要的类目(包括子类)
        /// </summary>
        /// <returns></returns>
        public List<ItemCat> GetItemCats()
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

          
        #endregion
    }
}
