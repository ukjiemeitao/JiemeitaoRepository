using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using com.shopstyle.api;
using com.shopstyle.bo;
using java.util;
using java.sql;
using java.text;

namespace ShopStyleAPIClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //DownloadBrands();
            //DownloadRetailers();                       
            //DownloadCategories();
            //DownloadColors();            
            //DownloadProducts();
            DownloadSizes("clothes-shoes-and-jewelry");
            //GetProductHistogram();
           
        }

      

        public static void DownloadSizes(string catParentID)
        {
            ShopStyle api = new ShopStyle("uid8409-24047347-36", ShopStyle.UK_API_HOSTNAME);

            using (CatalogDataContext dc = new CatalogDataContext())
            {
                var ssCatList = dc.SS_Categories.Where(c => c.parentid == catParentID);
                if (ssCatList != null)
                {
                    foreach (var cat in ssCatList)
                    {
                        try
                        {
                            var sizeResponse = api.getSizes(cat.cat_id);

                            foreach (var s in sizeResponse.getSizes())
                            {
                                SS_Size size = new SS_Size() { id = System.Guid.NewGuid(), cat_id = cat.cat_id, name = s.getName(), size_id = s.getId() };
                                dc.SS_Sizes.InsertOnSubmit(size);
                            }
                        }
                        catch (ShopStyle.APIException ex)
                        {
                            if (ex.getMessage().Contains("SizeFilter cannot be shown for category"))
                            {
                                DownloadSizes(cat.cat_id);
                            }
                            continue;
                        }

                    }
                    dc.SubmitChanges();
                }
                
            }
            
        }



        public static void DownloadRetailers()
        {
            ShopStyle api = new ShopStyle("uid8409-24047347-36", ShopStyle.UK_API_HOSTNAME);

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

        public static void DownloadBrands()
        {
            ShopStyle api = new ShopStyle("uid8409-24047347-36", ShopStyle.UK_API_HOSTNAME);

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

        public static void DownloadColors()
        {
            ShopStyle api = new ShopStyle("uid8409-24047347-36", ShopStyle.UK_API_HOSTNAME);

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
       
        public static void DownloadCategories()
        {
            ShopStyle api = new ShopStyle("uid8409-24047347-36", ShopStyle.UK_API_HOSTNAME);

            var catResponse = api.getCategories("fur-and-shearling-coats", 20);
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

        public static void GetProductHistogram()
        {
            ShopStyle api = new ShopStyle("uid8409-24047347-36", ShopStyle.UK_API_HOSTNAME);
            
            CatalogDataContext dc = new CatalogDataContext();
            SS_Category ssCat = dc.SS_Categories.FirstOrDefault<SS_Category>(c => c.cat_id == "jackets");
            SS_Brand ssBrand = dc.SS_Brands.FirstOrDefault<SS_Brand>(b => b.brand_id == 870);
            SS_Retailer ssRetailer = dc.SS_Retailers.FirstOrDefault<SS_Retailer>(r => r.retailer_id == 274);

            ProductQuery query = new ProductQuery().withCategory("jackets");
            Category cat = new Category(ssCat.cat_id, ssCat.name, ssCat.parentid);
            Brand brand = new Brand(ssBrand.brand_id,ssBrand.brand_name,ssBrand.url);
            Retailer retailer = new Retailer(ssRetailer.retailer_id,ssRetailer.name,ssRetailer.url,true);

            query.withCategory(cat); // Category jackets
            //query.withFilter(brand); // Brand filter id
            //query.withFilter(retailer);// Retailer filter id

            //SimpleDateFormat format = new SimpleDateFormat("yyyyMMdd");
            //java.util.Date parsed = format.parse("20130810");
            //java.sql.Date pdd = new java.sql.Date(parsed.getTime());

            //query.withPriceDropDate(pdd);
           
            ProductHistogramResponse response = api.getProductsHistogram(query,typeof(Price),typeof(Discount));

            PriceHistogramEntry[] arrPriHisEntry = response.getPriceHistogram();
        }
       

        public static void DownloadProducts()
        {
            ShopStyle api = new ShopStyle("uid8409-24047347-36", ShopStyle.UK_API_HOSTNAME);


            ProductQuery query = new ProductQuery().withCategory("jackets"); // Category jackets
            query.withFilter("b870"); // Brand filter id
            query.withFilter("r274");// Retailer filter id

            var productResponse = api.getProducts(query);

            ProductSearchMetadata metadata = productResponse.getMetadata();
            int total = metadata.getTotal(); // pageCounter = total/limit, these three values are being used to calculate paging. 
            int limit = metadata.getLimit();
            int offset = metadata.getOffset();

            foreach (var product in productResponse.getProducts())
            {
                using (CatalogDataContext dc = new CatalogDataContext())
                {
                    if(dc.SS_Products.FirstOrDefault(x => x.product_id == product.getId()) != null)
                    {
                        continue;
                    }
                }

                using(CatalogDataContext dc = new CatalogDataContext())
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
                        using(CatalogDataContext dc1 = new CatalogDataContext())
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
                    using(CatalogDataContext dc = new CatalogDataContext())
                    {
                        SS_Product ssProduct = dc.SS_Products.FirstOrDefault(x => x.product_id == product.getId());

                        if(ssProduct != null)
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
                     using(CatalogDataContext dc = new CatalogDataContext())
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
                                 if(dc1.SS_Images.FirstOrDefault(x =>x.image_id == pciMapping.image_id) == null)
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
                    using(CatalogDataContext dc = new CatalogDataContext())
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
    }
}
