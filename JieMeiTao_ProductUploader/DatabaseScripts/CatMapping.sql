with TaoBaoCategory_CTE (cid,parentname,parentcid,name,Level)
as(

select c1.cid,c2.Name, c1.parentcid,c1.name,1  from TB_ItemCat c1 inner join TB_ItemCat c2 on c1.ParentCid = c2.Cid where c1.parentcid = 50011740
UNION ALL
SELECT  tbc.cid, cte.name, tbc.parentcid,tbc.name,Level+1 FROM TB_ItemCat tbc inner join 
TaoBaoCategory_CTE cte on tbc.parentcid = cte.cid

)

select * from TaoBaoCategory_CTE;


with Category_CTE (id,parentid,cat_id,name,Level)
as(

select id,parentid, cat_id,name,1  from SS_Category where parentid = 'womens-clothes'
UNION ALL
SELECT SSC.id,SSC.parentid,SSC.cat_id,SSC.name,Level+1 FROM SS_Category SSC inner join 
Category_CTE cte on ssc.parentid = cte.cat_id

)

select id,parentid,cat_id,name,Level from Category_CTE