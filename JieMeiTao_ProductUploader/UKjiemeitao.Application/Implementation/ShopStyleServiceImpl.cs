using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Top.Api.Domain;
using UKjiemeitao.Application.DataObjects;
using UKjiemeitao.Application.Interface;
using UKjiemeitao.Domain.Model;
using UKjiemeitao.Domain.Repositories;
using UKjiemeitao.DataObjects;
using UKjiemeitao.Domain.Specifications;
using com.shopstyle.api;
using com.shopstyle.bo;

namespace UKjiemeitao.Application.Implementation
{
    /// <summary>
    /// 淘宝上传商品用到的Service
    /// </summary>
    public class ShopStyleServiceImpl : ApplicationService, IShopStyleService
    {
        #region 为了使用entityframework中的方法，必须在构造函数中初始化
        private readonly ISSProductRepository _SSProductRepository;
        private readonly ISSRetailerRepository _ISSRetailerRepository;

        public ShopStyleServiceImpl(IRepositoryContext context, ISSProductRepository ssProductRepository, ISSRetailerRepository ssRetailerRepository)
            : base(context)
        {
            this._SSProductRepository = ssProductRepository;
            this._ISSRetailerRepository = ssRetailerRepository;

        }
        protected override void Dispose(bool disposing)
        {
        }
        #endregion

        public SSProductDataObjectList GetProduct(DateTime startDate, DateTime endDate)
        {

            SSProductDataObjectList result = null;
            //
            var products = _SSProductRepository.FindAll(Specification<SS_Product>.Eval(o =>
                o.CreateDate.HasValue && o.CreateDate.Value.CompareTo(endDate) >= 0 && o.CreateDate.Value.CompareTo(startDate) <= 0));
            if (products != null)
            {
                result = new SSProductDataObjectList();
                foreach (var product in products)
                {
                    result.Add(Mapper.Map<SS_Product, SSProductDataObject>(product));  
                }
            }
            return result;
        }


        public bool TranslateProduct(SSProductDataObject product)
        {
            
            //var p = _SSProductRepository.GetByKey(Guid.Parse(product.id));
            var p = Mapper.Map<SSProductDataObject,SS_Product>(product);
            _SSProductRepository.Update(p);
            Context.Commit();
            return true;
        }


        public SSProductDataObject GetProductByID(string id)
        {
            var product = _SSProductRepository.GetByKey(Guid.Parse(id));
            var p = Mapper.Map<SS_Product, SSProductDataObject>(product);
            return p;
        }




        public void DownloadRetailers()
        {
            using (ShopStyleService api = new ShopStyleService())
            {
                foreach (Retailer r in api.GetRetailers())
                {
                    SS_Retailers retailer = new SS_Retailers() { Retailer_ID = r.getId(), Name = r.getName(), Url = r.getUrl(), DeepLinkSupport = r.isDeeplinkSupport() };
                    _ISSRetailerRepository.Add(retailer);
                    Context.Commit();
                }
               
            }
        }

        public void DownloadBrands()
        {
            throw new NotImplementedException();
        }

        public void DownloadColors()
        {
            throw new NotImplementedException();
        }

        public void DownloadSizes(string catParentID)
        {
            throw new NotImplementedException();
        }
    }
}
