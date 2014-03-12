using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UKjiemeitao.Domain.Model;
using UKjiemeitao.Domain.Repositories;

namespace UKjiemeitao.Application.Implementation
{
    public class TBProductAdapterService : ApplicationService
    {
        #region 为了使用entityframework中的方法，必须在构造函数中初始化
        private readonly IMappingCategoriesRepository _IMappingCategoriesRepository;
        private readonly ITBItemPropRepository _TBItemPropRepository;
        private readonly ITBPropValueRepository _TBPropValueRepository;
        private readonly ISSTBBrandMappingRepository _SSTBBrandMappingRepository;
        private readonly ISSImageRepository _SSImageRepository;

        public TBProductAdapterService(IRepositoryContext context, IMappingCategoriesRepository mappingCategoriesRepository, ITBItemPropRepository tbItemPropRepository
            , ITBPropValueRepository tbPropValueRepository, ISSTBBrandMappingRepository sstbBrandMappingRepository, ISSImageRepository ssImageRepository)
            : base(context)
        {
            this._IMappingCategoriesRepository = mappingCategoriesRepository;
            this._TBItemPropRepository = tbItemPropRepository;
            this._TBPropValueRepository = tbPropValueRepository;
            this._SSTBBrandMappingRepository = sstbBrandMappingRepository;
            this._SSImageRepository = ssImageRepository;
        }
        #endregion

        //先传puffer-coats分类下的
        public TB_Product ProductConvert(SS_Product ssproduct)
        {
            TB_Product tbproduct = new TB_Product();

            #region 需要计算的字段
            long? cid = null;
            #region cid

            var allitemcatemapping = _IMappingCategoriesRepository.FindAll();
            List<SS_Category> listcategory = ssproduct.CategoryCollection.ToList();
            if (listcategory != null && listcategory.Count > 0)
            {
                foreach (var item in listcategory)
                {
                    //判断Mapping_Categories是否有对应关系，如果有则可以找到对应的cid
                    var catemappingentity = allitemcatemapping.Where(i => i.ss_cid_array == item.Cat_ID).FirstOrDefault();
                    if (catemappingentity != null)
                    {
                        cid = catemappingentity.tb_cid;
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
            #region props
            //获取分类下的必填属性 现在能对应上的分类的关键属性pid只有2种：20000：品牌 1632501：货号
            var allItemProp = _TBItemPropRepository.FindAll();
            var mustItemProp = allItemProp.Where(i => i.Cid == cid && i.IsKeyProp == true).ToList();
            var allsstbBrandMapping = _SSTBBrandMappingRepository.FindAll();
            if (mustItemProp != null && mustItemProp.Count > 0)
            {
                foreach (var item in mustItemProp)
                {
                    if (item.Pid == 20000L)
                    { 
                        //品牌
                        SS_Brand ssBrand = ssproduct.Brand;
                        if (ssBrand != null)
                        {
                            SS_TB_Brand_Mapping sstbBrandMappingEntity = allsstbBrandMapping.Where(i => i.brand_name == ssBrand.Brand_Name && i.Cid == cid).FirstOrDefault();
                            if (sstbBrandMappingEntity != null)
                            {
                                listProp.Add(new KeyValuePair<string, string>(sstbBrandMappingEntity.Pid.ToString(), sstbBrandMappingEntity.Vid.ToString()));
                            }
                        }
                    }
                    else if (item.Pid == 1632501L)
                    {
                        //货号
                        TB_PropValue propValueEntity = _TBPropValueRepository.FindAll(i => i.Cid == cid && i.Pid == item.Pid).FirstOrDefault();
                        if (propValueEntity != null)
                        {
                            listProp.Add(new KeyValuePair<string, string>(propValueEntity.Pid.ToString(), propValueEntity.Vid.ToString()));
                        }
                    }
                    else
                    { 
                        //其它特殊必填属性，暂时没有
                    }
                }
            }
            #endregion
            if (listProp == null || listProp.Count != mustItemProp.Count)
            {
                return null;
            }
            tbproduct.Props = GetStrPidVid(listProp);
            #region ImgFilePath
            string ImgFilePath = "";
            string imgurl = _SSImageRepository.FindAll(i => i.Image_ID == ssproduct.Image_ID && i.Size_Name == "Small").FirstOrDefault().Url;
            SaveImage(imgurl, out ImgFilePath);
            tbproduct.ImgFilePath = ImgFilePath;
            #endregion
            tbproduct.PostageId = null;
            tbproduct.ChangeProp = null;
            #endregion

            tbproduct.Title = ssproduct.Name.Substring(0, 30);
            tbproduct.Desc = ssproduct.Description;
            tbproduct.Price = ssproduct.Price * 10;
            tbproduct.StuffStatus = "new";
            tbproduct.PropertyAlias = null;
            tbproduct.Num = 9999L;
            tbproduct.Type = null;
            tbproduct.LocationState = null;
            tbproduct.LocationCity = null;
            tbproduct.ApproveStatus = "onsale";
            tbproduct.FreightPayer = "buyer";
            tbproduct.ValidThru = null;
            tbproduct.HasInvoice = true;
            tbproduct.HasWarranty = false;
            tbproduct.HasShowcase = false;
            tbproduct.HasDiscount = false;
            tbproduct.PostFee = null;
            tbproduct.ExpressFee = null;
            tbproduct.EmsFee = null;
            tbproduct.ListTime = null;
            tbproduct.Increment = null;
            tbproduct.AuctionPoint = null;
            tbproduct.SkuProperties = null;
            tbproduct.SkuQuantities = null;
            tbproduct.SkuPrices = null;
            tbproduct.SkuOuterIds = null;
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
            tbproduct.GlobalStockType = null;
            tbproduct.GlobalStockCountry = null;
            tbproduct.NumIid = null;
            tbproduct.IsReplaceSku = null;
            tbproduct.EmptyFields = null;
            tbproduct.Status = 1;
            tbproduct.SellerCids = "841851052";
            tbproduct.PicPath = null;
            tbproduct.InputPids = null;
            tbproduct.InputStr = null;

            return tbproduct;
        }

        /// <summary>
        /// keyvaluepair转换成string
        /// </summary>
        /// <param name="convertlist"></param>
        /// <returns></returns>
        private string GetStrPidVid(List<KeyValuePair<string, string>> convertlist)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in convertlist)
            {
                sb.Append(item.Key + "," + item.Value+";");
            }
            return sb.ToString().Substring(0, sb.Length - 1);
        }

        /// <summary>
        /// 下载图片保存到本地
        /// </summary>
        /// <param name="imgurl"></param>
        /// <param name="filename"></param>
        private void SaveImage(string imgurl,out string filename)
        {
            WebClient mywebclient = new WebClient();
            string url = imgurl;
            filename = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".jpg";
            string filepath = @"C:\UKJieMeiTaoImage\" + filename;
            try
            {
                mywebclient.DownloadFile(url, filepath);
            }
            catch 
            {
                throw;
            }
        }

        protected override void Dispose(bool disposing)
        {

        }
    }
}
