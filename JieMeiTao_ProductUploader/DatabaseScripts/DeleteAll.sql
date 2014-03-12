-- delete test data
delete from SS_Product_Category_Mapping;
delete from SS_Product_Color_Image_Mapping;
delete from SS_Product_Size_Mapping
delete from TB_Product;
delete from SS_Product;
delete from SS_Images;
delete from Mapping_Categories;
delete from Mapping_Categories_Props;
delete from Mapping_Sizes;

--delete taobao categories, properties and property values

delete from TB_ItemCat;
delete from TB_ItemProp;
delete from TB_PropValue;



-- Don't need to delete following data.
delete from SS_Size;
delete from SS_Brand;
delete from SS_Brand_Synonyms;
delete from SS_Color;
delete from SS_Retailers;
delete from SS_Category;


