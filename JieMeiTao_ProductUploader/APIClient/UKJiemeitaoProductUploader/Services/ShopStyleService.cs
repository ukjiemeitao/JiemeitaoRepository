using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using com.shopstyle.bo;
using com.shopstyle.api;
using UKJiemeitaoProductUploader.DAL;

namespace UKJiemeitaoProductUploader.Services
{
    public class ShopStyleService:IDisposable
    {
        private ShopStyle api;
        private readonly string partnerKey = "uid8409-24047347-36";

        public ShopStyleService()
        {
            api = new ShopStyle(this.partnerKey, ShopStyle.UK_API_HOSTNAME);
        }
       

        public void DownloadRetailers()
        {
            
            var retailerResponse = api.getRetailers();
            using (CatalogDataContext dc = new CatalogDataContext())
            {
                foreach (Retailer retailer in retailerResponse.getRetailers())
                {
                    SS_Retailer ssRetailer = new SS_Retailer() { id = System.Guid.NewGuid(), name = retailer.getName(), retailer_id = (int)retailer.getId(), url = retailer.getUrl() };
                    dc.SS_Retailers.InsertOnSubmit(ssRetailer);
                }

                dc.SubmitChanges();
            }
            
        }

        public void DownloadBrands()
        {
            
            var brandResponse = api.getBrands();
            using (CatalogDataContext dc = new CatalogDataContext())
            {
                foreach (Brand brand in brandResponse.getBrands())
                {
                    SS_Brand ssBrand = new SS_Brand() { id = System.Guid.NewGuid(), brand_id = brand.getId(), brand_name = brand.getName(), url = brand.getUrl() };
                    dc.SS_Brands.InsertOnSubmit(ssBrand);
                }

                dc.SubmitChanges();
            }
          
        }

        public void DownloadColors()
        {
           
            var colorResponse = api.getColors();
            using (CatalogDataContext dc = new CatalogDataContext())
            {
                foreach (Color color in colorResponse.getColors())
                {
                    SS_Color ssColor = new SS_Color() { id = System.Guid.NewGuid(), color_id = color.getId(), color_name = color.getName(), url = color.getUrl() };
                    dc.SS_Colors.InsertOnSubmit(ssColor);
                }

                dc.SubmitChanges();
            }
           
        }

        public void DownloadCategories()
        {           

            var catResponse = api.getCategories("clothes-shoes-and-jewelry", 20);
            using (CatalogDataContext dc = new CatalogDataContext())
            {
                foreach (Category cat in catResponse.getCategories())
                {
                    SS_Category ssCat = new SS_Category() { id = System.Guid.NewGuid(), cat_id = cat.getId(), name = cat.getName(), parentid = cat.getParentId() };
                    dc.SS_Categories.InsertOnSubmit(ssCat);
                }

                dc.SubmitChanges();
            }
           
        }
        /// <summary>
        /// This method returns List<Price> and List<Discount> which are used 
        /// as datasource in DownloadSetting.asmx page. So the end user could
        /// select price range and discount range. Call this method before calling
        /// DownloadProduct method.
        /// </summary>
        /// <param name="catID"></param>
        /// <param name="brandFilterID"></param>
        /// <param name="retailerFilterID"></param>
        /// <param name="dscList"></param>
        /// <returns></returns>
        public List<Price> GetPriceAndDiscountRange(string catID, string brandFilterID,string retailerFilterID,out List<Discount> dscList)
        {
            ProductQuery query = new ProductQuery();
            List<Price> pList = new List<Price>();
            dscList = new List<Discount>();

            if (!string.IsNullOrEmpty(catID))
                query.withCategory(catID); // Category jackets
            if (!string.IsNullOrEmpty(brandFilterID))
                query.withFilter(brandFilterID); // Brand filter id
            if (!string.IsNullOrEmpty(retailerFilterID))
                query.withFilter(retailerFilterID);// Retailer filter id
           
                var histogramResponse = api.getProductsHistogram(query, typeof(Price), typeof(Discount));
                
                if (histogramResponse.getPriceHistogram() != null)
                {
                    foreach (var p in histogramResponse.getPriceHistogram())
                    {
                        pList.Add(p.getPrice());
                    }
                }
                if (histogramResponse.getDiscountHistogram() != null)
                {
                    foreach (var d in histogramResponse.getDiscountHistogram())
                    {
                        dscList.Add(d.getDiscount());
                    }
                }

            
            return pList;
        }

        public void DownloadProducts(string catID, string brandFilterID, string retailerFilterID,string priceFilterID, string discountFilterID)
        {

            ProductQuery query = new ProductQuery();

            if(!string.IsNullOrEmpty(catID))
                query.withCategory(catID); // Category jackets
            if(!string.IsNullOrEmpty(brandFilterID))
                query.withFilter(brandFilterID); // Brand filter id
            if(!string.IsNullOrEmpty(retailerFilterID))
                query.withFilter(retailerFilterID);// Retailer filter id
            if (!string.IsNullOrEmpty(priceFilterID))
                query.withFilter(priceFilterID); // Price filter id
            if (!string.IsNullOrEmpty(discountFilterID))
                query.withFilter(discountFilterID); // Discount filter id

            var productResponse = api.getProducts(query);

            ProductSearchMetadata metadata = productResponse.getMetadata();
            int total = metadata.getTotal(); // pageCounter = total/limit, these three values are being used to calculate paging. 
            int limit = metadata.getLimit();
            int offset = metadata.getOffset();

            foreach (var product in productResponse.getProducts())
            {
                using (CatalogDataContext dc = new CatalogDataContext())
                {
                    if (dc.SS_Products.FirstOrDefault(x => x.product_id == product.getId()) != null)
                    {
                        continue;
                    }
                }

                using (CatalogDataContext dc = new CatalogDataContext())
                {
                    SS_Product ssProduct = new SS_Product()
                    {
                        id = System.Guid.NewGuid(),
                        brand_id = (int?)product.getBrand().getId(),
                        click_url = product.getClickUrl(),
                        currency = product.getCurrency().getCurrencyCode(),
                        description = product.getDescription(),
                        image_id = product.getImage().getId(),
                        in_stock = product.isInStock(),
                        locale = product.getLocale().getDisplayCountry(),
                        name = product.getName(),
                        price = (decimal?)product.getPrice(),
                        price_label = product.getPriceLabel(),
                        retailer_id = (int?)product.getRetailer().getId(),
                        product_id = (int)product.getId(),
                        sale_price = (decimal?)product.getSalePrice() == 0 ? null : (decimal?)product.getSalePrice(),
                        sale_price_label = product.getSalePriceLabel()
                    };
                    dc.SS_Products.InsertOnSubmit(ssProduct);
                    dc.SubmitChanges();


                    foreach (ImageSize imageSize in product.getImage().getSizes().values().toArray())
                    {
                        using (CatalogDataContext dc1 = new CatalogDataContext())
                        {
                            SS_Image image = new SS_Image()
                            {
                                id = System.Guid.NewGuid(),
                                size_name = imageSize.getSizeName().toString(),
                                image_id = ssProduct.image_id,
                                url = imageSize.getUrl(),
                                height = imageSize.getHeight(),
                                width = imageSize.getWidth()
                            };
                            dc1.SS_Images.InsertOnSubmit(image);
                            dc1.SubmitChanges();

                        }
                    }


                }


                //Insert categories and product-category mapping records
                foreach (var category in product.getCategories())
                {
                    using (CatalogDataContext dc = new CatalogDataContext())
                    {
                        SS_Product ssProduct = dc.SS_Products.FirstOrDefault(x => x.product_id == product.getId());

                        if (ssProduct != null)
                        {
                            SS_Product_Category_Mapping pcMapping = new SS_Product_Category_Mapping() { id = System.Guid.NewGuid(), product_id = ssProduct.product_id, cat_id = category.getId() };
                            dc.SS_Product_Category_Mappings.InsertOnSubmit(pcMapping);
                            dc.SubmitChanges();
                        }
                        else
                        {
                            throw new ArgumentNullException("Could not find the newly inserted product");
                        }


                    }


                }

                //Isert product_color_image_mapping records
                foreach (var color in product.getColors())
                {
                    using (CatalogDataContext dc = new CatalogDataContext())
                    {
                        SS_Product_Color_Image_Mapping pciMapping = new SS_Product_Color_Image_Mapping()
                        {
                            id = System.Guid.NewGuid(),
                            color_name = color.getName(),
                            image_id = color.getImage().getId(),
                            product_id = (int)product.getId()
                        };

                        foreach (ImageSize imageSize in color.getImage().getSizes().values().toArray())
                        {
                            using (CatalogDataContext dc1 = new CatalogDataContext())
                            {
                                if (dc1.SS_Images.FirstOrDefault(x => x.image_id == pciMapping.image_id) == null)
                                {
                                    SS_Image image = new SS_Image()
                                    {
                                        id = System.Guid.NewGuid(),
                                        size_name = imageSize.getSizeName().toString(),
                                        image_id = pciMapping.image_id,
                                        url = imageSize.getUrl(),
                                        height = imageSize.getHeight(),
                                        width = imageSize.getWidth()
                                    };
                                    dc1.SS_Images.InsertOnSubmit(image);
                                    dc1.SubmitChanges();
                                }
                            }
                        }

                        dc.SS_Product_Color_Image_Mappings.InsertOnSubmit(pciMapping);
                        dc.SubmitChanges();
                    }

                }

                //Insert size and product_size_mapping
                foreach (var size in product.getSizes())
                {
                    using (CatalogDataContext dc = new CatalogDataContext())
                    {
                        SS_Size result = dc.SS_Sizes.FirstOrDefault(x => x.name == size.getName());

                        if (result == null)
                        {
                            System.Guid sizeId;
                            using (CatalogDataContext dc1 = new CatalogDataContext())
                            {
                                SS_Size s = new SS_Size() { id = System.Guid.NewGuid(), name = size.getName() };
                                dc1.SS_Sizes.InsertOnSubmit(s);
                                dc1.SubmitChanges();

                                sizeId = s.id;
                            }

                            using (CatalogDataContext dc2 = new CatalogDataContext())
                            {
                                SS_Product_Size_Mapping psMapping = new SS_Product_Size_Mapping() { id = System.Guid.NewGuid(), product_id = (int)product.getId(), size_id = sizeId };
                                dc2.SS_Product_Size_Mappings.InsertOnSubmit(psMapping);
                                dc2.SubmitChanges();
                            }


                        }
                        else
                        {
                            SS_Product_Size_Mapping psMapping = new SS_Product_Size_Mapping() { id = System.Guid.NewGuid(), product_id = (int)product.getId(), size_id = result.id };
                            dc.SS_Product_Size_Mappings.InsertOnSubmit(psMapping);
                            dc.SubmitChanges();
                        }
                    }
                }
            }           
        }

        public void Dispose()
        {
            api.close();
        }
    }
}