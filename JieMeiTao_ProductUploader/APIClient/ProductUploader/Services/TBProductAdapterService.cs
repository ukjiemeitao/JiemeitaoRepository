using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ProductUploader.DAL;
using System.Configuration;


namespace ProductUploader.Services
{
    public class TBProductAdapterService
    {


        //先传puffer-coats分类下的
        public TB_Product ProductConvert(SS_Product ssproduct)
        {
            using (CatalogDataContext dct = new CatalogDataContext())
            {
                TB_Product tbproduct = new TB_Product();

                #region 需要计算的字段
                long? cid = null;
                #region cid

                var allitemcatemapping = dct.Mapping_Categories;
                var listcategory = from mapping in ssproduct.SS_Product_Category_Mappings select mapping.SS_Category;
                if (listcategory != null && listcategory.ToList<SS_Category>().Count > 0)
                {
                    foreach (var item in listcategory)
                    {
                        //判断Mapping_Categories是否有对应关系，如果有则可以找到对应的cid
                        var catemappingentity = allitemcatemapping.Where(i => i.ss_cat_id == item.cat_id).FirstOrDefault();

                        if (catemappingentity != null)
                        {
                            cid = catemappingentity.tb_cid;
                            break;
                        }
                        //如果没有找到分类的对应关系，查看关键字和分类的组合
                        var keywordCatEntity = from mapping in allitemcatemapping where mapping.keyword == ssproduct.keyword && mapping.ss_cat_id == item.cat_id select mapping.tb_cid;

                        if (keywordCatEntity != null && keywordCatEntity.Count() > 0)
                        {
                            cid = keywordCatEntity.First();
                            break;
                        }
                    }
                }
                #endregion
                if (cid == null)
                {
                    return null;
                }
                tbproduct.Cid = cid;
                List<KeyValuePair<string, string>> listProp = new List<KeyValuePair<string, string>>();
                List<KeyValuePair<string, string>> listInputProp = new List<KeyValuePair<string, string>>();
                List<KeyValuePair<string, string>> listColors = new List<KeyValuePair<string, string>>();
                List<KeyValuePair<string, string>> listSizes = new List<KeyValuePair<string, string>>();

                #region props
                //获取分类下的必填属性 
                var allItemProp = dct.TB_ItemProps;
                var keyItemProp = allItemProp.Where(i => i.Cid == cid && i.IsKeyProp == true).ToList();
                var allsstbBrandMapping = dct.SS_TB_Brand_Mappings;


                foreach (var item in keyItemProp)
                {
                    if (item.Name.Contains("货号"))
                    {
                        listInputProp.Add(new KeyValuePair<string, string>(item.Pid.ToString(), "ss_" + ssproduct.product_id.ToString()));//用SS商品ID作为货号，可以用来搜索
                    }
                    else if (item.Name.Contains("品牌"))
                    {
                        //品牌
                        SS_Brand ssBrand = ssproduct.SS_Brand;
                        if (ssBrand != null)
                        {
                            //var DD = allsstbBrandMapping.Where(i => i.brand_name.Equals(ssBrand.brand_name, StringComparison.CurrentCultureIgnoreCase)).ToList();
                            //var ee = allsstbBrandMapping.Where(i => i.Cid == cid).ToList();
                            SS_TB_Brand_Mapping sstbBrandMappingEntity = allsstbBrandMapping.Where(i => i.brand_name == ssBrand.brand_name && i.Cid == cid).ToList().FirstOrDefault();
                            if (sstbBrandMappingEntity != null)
                            {
                                listProp.Add(new KeyValuePair<string, string>(sstbBrandMappingEntity.Pid.ToString(), sstbBrandMappingEntity.Vid.ToString()));
                            }
                            else
                            {
                                listInputProp.Add(new KeyValuePair<string, string>(item.Pid.ToString(), ssproduct.SS_Brand.brand_name));
                            }
                        }
                    }
                    else
                    {
                        //其它重要属性，面料主成分含量，质地
                    }
                }


                //获得颜色分类属性, 颜色分类属性Pid 1627207，同时获得颜色别名  
                StringBuilder sbAlias = new StringBuilder();

                foreach (var ssColor in ssproduct.SS_Product_Color_Image_Mappings)
                {
                    var colorProp = (from prop in dct.TB_ItemProps where prop.Cid == cid && prop.IsSaleProp == true && prop.Name.Contains("颜色") select prop).Single();                   
                    var color = new KeyValuePair<string, string>(colorProp.Pid.ToString(), dct.Mapping_Colors.Where(m => m.ss_color == ssColor.color_name && m.tb_cid == cid).Single().tb_vid.ToString());
                    listProp.Add(color);
                    listColors.Add(color);
                    sbAlias.Append(color.Key + ":" + color.Value + ":" + ssColor.color_name + ";");
                }
              
                //获得尺寸分类属性               
                foreach (var m in ssproduct.SS_Product_Size_Mappings)
                {
                    var sizeProp = dct.TB_PropValues.Where(pv => pv.Cid == cid && (pv.Name.Contains("尺码") || pv.Name.Contains("尺寸") || pv.Name.Contains("包袋大小") || pv.Name.Contains("长度") || pv.Name.Contains("周长")) && pv.Name == m.SS_Size.name).SingleOrDefault();

                    if ( sizeProp != null)
                    {                       
                        var s = new KeyValuePair<string, string>(sizeProp.Pid.ToString(), sizeProp.Vid.ToString());
                        listProp.Add(s);
                        listSizes.Add(s);
                    }
                    else
                    {
                        //如果在淘宝属性值里找不到对应的尺寸，那么到尺寸的对应表里去找值。
                        var sProp = (from prop in dct.TB_ItemProps where prop.Cid == cid && prop.IsSaleProp == true && (prop.Name.Contains("尺码") || prop.Name.Contains("尺寸") || prop.Name.Contains("包袋大小") || prop.Name.Contains("长度") || prop.Name.Contains("周长")) select prop).SingleOrDefault();
                        var size = dct.Mapping_Sizes.Where(tm => tm.tb_cid == cid && tm.ss_size_name == m.SS_Size.name).FirstOrDefault();
                        if (size != null && sProp != null)
                        {
                            var s = new KeyValuePair<string, string>(sProp.Pid.ToString(), size.tb_vid.ToString());
                            listProp.Add(s);
                            listSizes.Add(s);
                            sbAlias.Append(s.Key + ":" + s.Value + ":" + m.SS_Size.name + ";");
                        }
                    }
                }

                //获得必填属性

                var mustProps = from props in dct.TB_ItemProps where props.Must == true && props.Cid == cid select props;

                foreach (var prop in mustProps)
                {
                    var mustPropMapping = from mcp in dct.Mapping_Categories_Props where mcp.Mapping_Category.tb_cid == prop.Cid && mcp.pid == prop.Pid select mcp;

                    if (mustPropMapping.SingleOrDefault() != null)
                    {
                        var m = new KeyValuePair<string, string>(prop.Pid.ToString(), mustPropMapping.Single().vid.ToString());
                        listProp.Add(m);
                    }
                    else
                        throw new ApplicationException("没有找到匹配的淘宝必填属性");
                }

                //计算sku属性
                string strSkuProps = GetSkuPidVid(listColors, listSizes);
                int countSku = string.IsNullOrEmpty(strSkuProps) == true ? 0 : strSkuProps.Split(new char[] { ',' }).Count();

                #endregion
                //if (listProp == null || listProp.Count != mustItemProp.Count - 1)
                //{
                //    return null;
                //}

                tbproduct.Props = GetStrPidVid(listProp);
                #region ImgFilePath
                string ImgFilePath = "";
                string imgurl = dct.SS_Images.Where(i => i.image_id == ssproduct.image_id && i.size_name == "Original").FirstOrDefault().url;
                Utils.SaveImage(imgurl, out ImgFilePath);
                tbproduct.ImgFilePath = ImgFilePath;
                #endregion
                tbproduct.PostageId = 804240630;//180-实际80元运费
                tbproduct.ChangeProp = null;
                #endregion

                decimal exchangeRate = decimal.Parse(ConfigurationManager.AppSettings["ExchangeRate"]);
                double normalRate = double.Parse(ConfigurationManager.AppSettings["NormalMarginRate"]);
                double saleRate = double.Parse(ConfigurationManager.AppSettings["SaleMarginRate"]); 


                tbproduct.Title = ssproduct.chinese_name; //Todo: 不能超过30字符.
                tbproduct.Desc = ssproduct.chinese_description;//字数要大于5个字符，小于25000个字符
                if (ssproduct.sale_price == null)
                {
                    tbproduct.Price = Math.Round(Utils.CalculateSellPrice(ssproduct.price, normalRate, exchangeRate));
                }
                else
                {
                    tbproduct.Price = Math.Round( Utils.CalculateSellPrice(ssproduct.sale_price, saleRate, exchangeRate));
                }
                tbproduct.StuffStatus = "new";
                tbproduct.PropertyAlias = string.IsNullOrEmpty(sbAlias.ToString()) == false ? sbAlias.ToString().Substring(0, sbAlias.Length - 1):string.Empty;
                tbproduct.Num = countSku == 0 ? 5 : 5 * countSku;
                tbproduct.Type = "fixed";
                tbproduct.LocationState = "海外";
                tbproduct.LocationCity = "海外";
                tbproduct.ApproveStatus = "instock";//可选值:onsale(出售中),instock(仓库中)
                tbproduct.FreightPayer = "buyer";
                tbproduct.InputPids = GetInputKeys(listInputProp);
                tbproduct.InputStr = GetInputValues(listInputProp);
                tbproduct.ValidThru = 7;
                tbproduct.HasInvoice = false;
                tbproduct.HasWarranty = false;
                tbproduct.HasShowcase = false;
                tbproduct.HasDiscount = false;                
                tbproduct.PostFee = 80;
                tbproduct.ExpressFee = 600;
                tbproduct.EmsFee = 600;
                tbproduct.ListTime = null;
                tbproduct.Increment = null;
                tbproduct.AuctionPoint = null;
                tbproduct.SkuProperties = strSkuProps;
                tbproduct.SkuQuantities = GetSkuProps("5", countSku);
                tbproduct.SkuPrices = GetSkuProps(tbproduct.Price.ToString(), countSku);
                tbproduct.SkuOuterIds = GetSkuProps("", countSku);
                tbproduct.Lang = "zh_CN";
                tbproduct.OuterId = null;
                tbproduct.IsTaobao = true;
                tbproduct.IsEx = false;
                tbproduct.Is3D = false;
                tbproduct.SellPromise = false;
                tbproduct.AfterSaleId = null;
                tbproduct.CodPostageId = null;
                tbproduct.IsLightningConsignment = false;
                tbproduct.Weight = null;
                tbproduct.IsXinpin = null;
                tbproduct.SubStock = 2L;
                tbproduct.ItemSize = null;
                tbproduct.ItemWeight = null;
                tbproduct.DescModules = null;
                tbproduct.GlobalStockType = "2";//值为2时代表代购 
                tbproduct.GlobalStockCountry = "英国";
                tbproduct.NumIid = null;
                tbproduct.IsReplaceSku = null;
                tbproduct.EmptyFields = null;
                tbproduct.Status = 1;
                tbproduct.SellerCids = ssproduct.SS_Product_Set.tb_seller_cid.ToString(); //"841851052"; //TODO: 建立新的分类，名称是ProductSetName
                tbproduct.PicPath = null;
                tbproduct.SSProductID = ssproduct.product_id;
                tbproduct.IsUploaded = false;
                tbproduct.ID = System.Guid.NewGuid();


                return tbproduct;
            }

        }

        /// <summary>
        /// keyvaluepair转换成string
        /// </summary>
        /// <param name="convertlist"></param>
        /// <returns></returns>
        private string GetStrPidVid(List<KeyValuePair<string, string>> convertlist)
        {
            if (convertlist.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in convertlist)
                {
                    sb.Append(item.Key + ":" + item.Value + ";");
                }
                return sb.ToString().Substring(0, sb.Length - 1);
            }
            else
                return null;

        }

        private string GetSkuPidVid(List<KeyValuePair<string, string>> listColors, List<KeyValuePair<string, string>> listSizes)
        {
            if (listColors.Count > 0 && listSizes.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var color in listColors)
                {
                    foreach (var size in listSizes)
                    {
                        sb.Append(color.Key + ":" + color.Value + ";" + size.Key + ":" + size.Value + ",");
                    }
                }

                return sb.ToString().Substring(0, sb.Length - 1);
            }
            else
                return null;

        }

        private string GetSkuProps(string prop, int countSku)
        {
            if (countSku > 0)
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < countSku; i++)
                {
                    sb.Append(prop + ",");
                }

                return sb.ToString().Substring(0, sb.Length - 1);
            }
            else
                return null;
        }

        private string GetInputKeys(List<KeyValuePair<string, string>> listInput)
        {
            if (listInput.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var item in listInput)
                {
                    sb.Append(item.Key + ",");
                }

                return sb.ToString().Substring(0, sb.Length - 1);
            }
            else
                return null;
        }

        private string GetInputValues(List<KeyValuePair<string, string>> listInput)
        {
            if (listInput.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var item in listInput)
                {
                    sb.Append(item.Value + ",");
                }

                return sb.ToString().Substring(0, sb.Length - 1);
            }
            else
                return null;
        }
    }
}