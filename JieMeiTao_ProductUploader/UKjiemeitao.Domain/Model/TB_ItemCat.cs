using System;
using System.Collections.Generic;

namespace UKjiemeitao.Domain.Model
{
    public partial class TB_ItemCat : AggregateRoot
    {
        public Nullable<long> Cid { get; set; }
        public string Name { get; set; }
        public Nullable<bool> IsParent { get; set; }
        public Nullable<System.DateTime> ModifiedTime { get; set; }
        public string ModifiedType { get; set; }
        public Nullable<long> ParentCid { get; set; }
        public Nullable<long> SortOrder { get; set; }
        public string Status { get; set; }
    }
}
