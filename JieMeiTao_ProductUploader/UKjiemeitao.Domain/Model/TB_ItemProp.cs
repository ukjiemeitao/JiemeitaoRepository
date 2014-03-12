using System;
using System.Collections.Generic;

namespace UKjiemeitao.Domain.Model
{
    public partial class TB_ItemProp : AggregateRoot
    {
        public Nullable<long> Cid { get; set; }
        public Nullable<long> Pid { get; set; }
        public Nullable<long> ParentPid { get; set; }
        public Nullable<long> ParentVid { get; set; }
        public string ChildTemplate { get; set; }
        public Nullable<bool> IsAllowAlias { get; set; }
        public Nullable<bool> IsColorProp { get; set; }
        public Nullable<bool> IsEnumProp { get; set; }
        public Nullable<bool> IsInputProp { get; set; }
        public Nullable<bool> IsItemProp { get; set; }
        public Nullable<bool> IsKeyProp { get; set; }
        public Nullable<bool> IsSaleProp { get; set; }
        public Nullable<System.DateTime> ModifiedTime { get; set; }
        public string ModifiedType { get; set; }
        public Nullable<bool> Multi { get; set; }
        public Nullable<bool> Must { get; set; }
        public string Name { get; set; }
        public Nullable<bool> Required { get; set; }
        public Nullable<long> SortOrder { get; set; }
        public string Status { get; set; }
    }
}
