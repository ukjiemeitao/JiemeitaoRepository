select * from SS_Product sp inner join
SS_Product_Category_Mapping spcm on sp.product_id = spcm.product_id
inner join SS_Category sc on sc.cat_id = spcm.cat_id
where sp.product_id = 435118586

