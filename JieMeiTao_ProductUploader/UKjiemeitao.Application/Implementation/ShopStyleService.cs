using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.shopstyle.api;
using com.shopstyle.bo;

namespace UKjiemeitao.Application.Implementation
{
    public class ShopStyleService : IDisposable
    {
        private ShopStyle api;
        private readonly string partnerKey = "uid8409-24047347-36";

        public ShopStyleService()
        {
            api = new ShopStyle(this.partnerKey, ShopStyle.UK_API_HOSTNAME);
        }

        #region Download Methods
        public List<Retailer> GetRetailers()
        {

            var retailerResponse = api.getRetailers();
            return retailerResponse.getRetailers().ToList<Retailer>();

        }

        public List<Brand> GetBrands()
        {

            var brandResponse = api.getBrands();
            return brandResponse.getBrands().ToList<Brand>();

        }

        public List<Color> GetColors()
        {

            var colorResponse = api.getColors();
            return colorResponse.getColors().ToList<Color>();
        }

        public List<Size> GetSizesByCategory(string catID)
        {

            var sizeResponse = api.getSizes(catID);
            return sizeResponse.getSizes().ToList<Size>();
        }

        public List<Category> GetCategories( string catID, int depth)
        {

            var catResponse = api.getCategories(catID, depth);
            return catResponse.getCategories().ToList<Category>();

        }

        public List<Product> GetProducts(string fts, string catID, string brandFilterID, string retailerFilterID, string priceFilterID, string discountFilterID)
        {
            List<Product> pList = new List<Product>();

            ProductQuery query = new ProductQuery();
            if (!string.IsNullOrEmpty(fts))
                query.withFreeText(fts);
            if (!string.IsNullOrEmpty(catID))
                query.withCategory(catID); // Category jackets
            if (!string.IsNullOrEmpty(brandFilterID))
                query.withFilter(brandFilterID); // Brand filter id
            if (!string.IsNullOrEmpty(retailerFilterID))
                query.withFilter(retailerFilterID);// Retailer filter id
            if (!string.IsNullOrEmpty(priceFilterID))
                query.withFilter(priceFilterID); // Price filter id
            if (!string.IsNullOrEmpty(discountFilterID))
                query.withFilter(discountFilterID); // Discount filter id

            var response = api.getProducts(query);

            ProductSearchMetadata metadata = response.getMetadata();
            int total = metadata.getTotal(); // pageCounter = total/limit, these three values are being used to calculate paging. 
            int limit = metadata.getLimit();
            int offset = metadata.getOffset();
            int pageCount = (int)Math.Ceiling((double)total / 100);// 100 records for each page.

            for (int i = 0; i < pageCount; i++)
            {
                PageRequest page = new PageRequest().withLimit(100).withOffset(offset);
                var productResponse = api.getProducts(query, page, ProductSort.PriceLoHi);

                pList.AddRange(productResponse.getProducts().ToList<Product>());
                
                offset += 100;
            }

            return pList;

        }
       
        public List<PriceHistogramEntry> GetPriceHistogram(string catID, string brandFilterID, string retailerFilterID)
        {
            ProductQuery query = new ProductQuery();
          
            if (!string.IsNullOrEmpty(catID))
                query.withCategory(catID); // Category jackets
            if (!string.IsNullOrEmpty(brandFilterID))
                query.withFilter(brandFilterID); // Brand filter id
            if (!string.IsNullOrEmpty(retailerFilterID))
                query.withFilter(retailerFilterID);// Retailer filter id

            var priceResponse = api.getProductsHistogram(query, typeof(Price));

            return priceResponse.getPriceHistogram().ToList<PriceHistogramEntry>();


           
        }

        public List<DiscountHistogramEntry> GetDiscountHistogram(string catID, string brandFilterID, string retailerFilterID)
        {
            ProductQuery query = new ProductQuery();
           
            if (!string.IsNullOrEmpty(catID))
                query.withCategory(catID); // Category jackets
            if (!string.IsNullOrEmpty(brandFilterID))
                query.withFilter(brandFilterID); // Brand filter id
            if (!string.IsNullOrEmpty(retailerFilterID))
                query.withFilter(retailerFilterID);// Retailer filter id
           
            var discountResponse = api.getProductsHistogram(query, typeof(Discount));

            return discountResponse.getDiscountHistogram().ToList<DiscountHistogramEntry>();                                     
        }

       
        #endregion



        public void Dispose()
        {
            api.close();
        }
    }
}
