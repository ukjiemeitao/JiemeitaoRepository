SELECT     distinct(TB_ItemProp.Name), TB_ItemProp.Pid, TB_ItemCat.Name,TB_ItemCat.Cid
FROM         TB_ItemCat INNER JOIN
                      TB_ItemProp ON TB_ItemCat.Cid = TB_ItemProp.Cid INNER JOIN
                      TB_PropValue ON TB_ItemProp.Pid = TB_PropValue.Pid
WHERE     ((TB_ItemProp.IsKeyProp = 1) OR
                      (TB_ItemProp.Must = 1)) and TB_ItemCat.Cid in (1623,50000697
,1629,50008898,50008899,50011404,162116,50013194,50000852
,50008905
,162205
,50011277
,50000671
,50008904
,50013196
,50008901
,162103
) order by TB_ItemCat.Cid
                      