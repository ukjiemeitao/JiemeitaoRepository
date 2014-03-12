with Category_CTE (id,parentid,cat_id,name,Level)
as(

select id,parentid, cat_id,name,1  from SS_Category where parentid = 'clothes-shoes-and-jewelry'
UNION ALL
SELECT SSC.id,SSC.parentid,SSC.cat_id,SSC.name,Level+1 FROM SS_Category SSC inner join 
Category_CTE cte on ssc.parentid = cte.cat_id

)

select id,parentid,cat_id,name,Level from Category_CTE