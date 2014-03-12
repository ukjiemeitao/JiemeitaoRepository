java-shopstyle-api
==========================

Java ShopStyle API.

Sample code:

    ShopStyle api = new ShopStyle("YOUR_KEY");
    ProductQuery query = new ProductQuery().withFreeText("red dresses");
    ProductSearchResponse response = api.getProducts(query);
    for (Product product : response.getProducts()) {
        System.out.println(product.getName());
    }

    ProductHistogramResponse histograms = api.getProductsHistogram(query, Category.class, Retailer.class);
    CategoryHistogramEntry[] categoryHistogram = histograms.getCategoryHistogram();
    RetailerHistogramEntry[] retailerHistogram = histograms.getRetailerHistogram();
