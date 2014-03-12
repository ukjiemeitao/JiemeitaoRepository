using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using com.shopstyle.api;
using com.shopstyle.bo;
using ProductUploader.DAL;
using HtmlAgilityPack;
using System.Text;

namespace ProductUploader.Services
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

        public void DownloadProducts(string fts, string catID, string brandFilterID, string retailerFilterID, string priceFilterID, string discountFilterID, string productSetName)
        {

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
            int pageCount = (int)Math.Ceiling((double)total / 100);

            System.Guid productSetID;

            using (CatalogDataContext context = new CatalogDataContext()) 
            {
                var ps = (from set in context.SS_Product_Sets where set.product_set_name == productSetName select set).SingleOrDefault();
                if (ps == null)
                {
                    SS_Product_Set productSet = new SS_Product_Set() { id = System.Guid.NewGuid(), product_set_name = productSetName, tb_seller_cid = ProductService.AddSellerCat(productSetName).Cid, datetimecreated = (DateTime ?)DateTime.Now };
                    
                    context.SS_Product_Sets.InsertOnSubmit(productSet);
                    context.SubmitChanges();
                    productSetID = productSet.id;
                }
                else
                    productSetID = ps.id;

               
            }
                                   
            for (int i = 0; i < pageCount; i++)
            {
                PageRequest page = new PageRequest().withLimit(100).withOffset(offset);
                var productResponse = api.getProducts(query, page, ProductSort.PriceLoHi);

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
                            chinese_description = getTranslateResult(stripOffHTMLTags(product.getDescription()), true) ?? string.Empty,
                            image_id = product.getImage().getId(),
                            in_stock = product.isInStock(),
                            locale = product.getLocale().getDisplayCountry(),
                            name = product.getName(),
                            chinese_name = product.getBrand().getName() + getTranslateResult(product.getName(), false) ?? string.Empty,
                            price = (decimal?)product.getPrice(),
                            price_label = product.getPriceLabel(),
                            retailer_id = (int?)product.getRetailer().getId(),
                            product_id = (int)product.getId(),
                            sale_price = (decimal?)product.getSalePrice() == 0 ? null : (decimal?)product.getSalePrice(),
                            sale_price_label = product.getSalePriceLabel(),
                            istranslated = false,
                            keyword = fts,
                            datetimecreated = DateTime.Now,
                            product_set_name_id = productSetID
                        };
                        dc.SS_Products.InsertOnSubmit(ssProduct);
                        dc.SubmitChanges();





                    }

                    using (CatalogDataContext dc = new CatalogDataContext())
                    {
                        SS_Product ssProduct = dc.SS_Products.FirstOrDefault(x => x.product_id == product.getId());

                        //Insert categories and product-category mapping records
                        if (!string.IsNullOrEmpty(catID))
                        {
                            SS_Product_Category_Mapping pcMapping = new SS_Product_Category_Mapping() { id = System.Guid.NewGuid(), product_id = ssProduct.product_id, cat_id = catID };
                            dc.SS_Product_Category_Mappings.InsertOnSubmit(pcMapping);
                            dc.SubmitChanges();
                        }
                        else
                        {
                            foreach (var category in product.getCategories())
                            {
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
                    }

                    //insert main image
                    foreach (ImageSize imageSize in product.getImage().getSizes().values().toArray())
                    {
                        using (CatalogDataContext dc1 = new CatalogDataContext())
                        {
                            SS_Product ssProduct = dc1.SS_Products.FirstOrDefault(x => x.product_id == product.getId());


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

                    //Isert product_color_image_mapping records
                    foreach (var color in product.getColors())
                    {
                        using (CatalogDataContext dc = new CatalogDataContext())
                        {

                            SS_Product ssProduct = dc.SS_Products.FirstOrDefault(x => x.product_id == product.getId());

                            SS_Product_Color_Image_Mapping pciMapping = new SS_Product_Color_Image_Mapping();
                            pciMapping.id =  System.Guid.NewGuid();
                            pciMapping.color_name = color.getName();
                            pciMapping.product_id = (int)product.getId();

                            if (color.getImage() == null)
                            {
                                pciMapping.image_id = ssProduct.image_id;//if there's no image element inside color element, that means only one color available.
                            }
                            else
                            {
                                pciMapping.image_id =  color.getImage().getId();
                            }                                                           

                            

                            if (color.getImage() != null)
                            {
                                var ssImage = (from image in dc.SS_Images where image.image_id == color.getImage().getId() select image).FirstOrDefault();

                                if (ssImage == null)
                                {
                                    foreach (ImageSize imageSize in color.getImage().getSizes().values().toArray())
                                    {
                                        using (CatalogDataContext dc1 = new CatalogDataContext())
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
                offset += 100;
            }
        }
        #endregion


        #region Get Methods
        /// <summary>
        /// This method returns List<SS_Price> and List<SS_Discount> which are used 
        /// as datasource in DownloadSetting.asmx page. So the end user could
        /// select price range and discount range. Call this method before calling
        /// DownloadProduct method.
        /// </summary>
        /// <param name="catID"></param>
        /// <param name="brandFilterID"></param>
        /// <param name="retailerFilterID"></param>
        /// <param name="dscList"></param>
        /// <returns></returns>
        public List<SS_Price> GetPriceAndDiscountRange(string catID, string brandFilterID, string retailerFilterID, out List<SS_Discount> dscList)
        {
            ProductQuery query = new ProductQuery();
            List<SS_Price> pList = new List<SS_Price>();
            dscList = new List<SS_Discount>();

            if (!string.IsNullOrEmpty(catID))
                query.withCategory(catID); // Category jackets
            if (!string.IsNullOrEmpty(brandFilterID))
                query.withFilter(brandFilterID); // Brand filter id
            if (!string.IsNullOrEmpty(retailerFilterID))
                query.withFilter(retailerFilterID);// Retailer filter id

            var priceResponse = api.getProductsHistogram(query, typeof(Price));
            var discountResponse = api.getProductsHistogram(query, typeof(Discount));

            if (priceResponse.getPriceHistogram() != null)
            {
                foreach (var p in priceResponse.getPriceHistogram())
                {
                    pList.Add(new SS_Price() { FilterID = p.getPrice().getFilterId(), Name = p.getPrice().getName(), URL = p.getPrice().getUrl() });
                }
            }
            if (discountResponse.getDiscountHistogram() != null)
            {
                foreach (var d in discountResponse.getDiscountHistogram())
                {
                    dscList.Add(new SS_Discount() { FilterID = d.getDiscount().getFilterId(), Name = d.getDiscount().getName(), URL = d.getDiscount().getUrl() });
                }
            }


            return pList;
        }

        public List<SS_Size> GetSizesByCategory(string catID)
        {
            List<SS_Size> sList = new List<SS_Size>();
            var sizeResponse = api.getSizes(catID);
            foreach (var size in sizeResponse.getSizes())
            {
                sList.Add(new SS_Size() { size_id = size.getFilterId(), name = size.getName(), cat_id = catID });

            }

            return sList;
        }
        #endregion

        private string getTranslateResult(string source, bool isHtml)
        {
            BaiDuTranslateService trans = new BaiDuTranslateService();
            var list = trans.Translate(source, "en", "zh");
            StringBuilder result = new StringBuilder();
            if (list != null)
            {
                foreach (var item in list)
                {
                    if (isHtml)
                        result.Append(@"<p align=""center""><span style=""color:#ff00ff;""><span style=""font-family:comic sans ms;font-size:18.0pt;""><font color=""#ff00ff"" face=""黑体"">");
                    result.Append(item.dst);
                    if (isHtml)
                        result.Append("</font></span></span></p>");

                }
                return result.ToString();
            }
            else
                return null;
        }

        private string stripOffHTMLTags(string strHtml)
        {
            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(strHtml);
            string strSource = getSourceString(html.DocumentNode).Replace("&", " and ");

            return strSource;
        }

        private string getSourceString(HtmlNode node)
        {
            StringBuilder strSource = new StringBuilder();

            if (node.HasChildNodes)
            {
                foreach (var child in node.ChildNodes)
                {
                    if (!child.HasChildNodes)
                    {
                        strSource.Append(child.InnerText);
                        strSource.Append("\n");
                    }
                    else
                    {
                        strSource.Append(getSourceString(child));
                    }
                }
            }

            return strSource.ToString();
        }

        public void Dispose()
        {
            api.close();
        }
    }
}