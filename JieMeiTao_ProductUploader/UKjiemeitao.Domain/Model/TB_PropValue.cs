using System;
using System.Collections.Generic;

namespace UKjiemeitao.Domain.Model
{
    public partial class TB_PropValue : AggregateRoot
    {
        public Nullable<long> Cid { get; set; }
        public Nullable<bool> IsParent { get; set; }
        public Nullable<System.DateTime> ModifiedTime { get; set; }
        public string ModifiedType { get; set; }
        public string Name { get; set; }
        public string NameAlias { get; set; }
        public Nullable<long> Pid { get; set; }
        public string PropName { get; set; }
        public Nullable<long> SortOrder { get; set; }
        public string Status { get; set; }
        public Nullable<long> Vid { get; set; }
    }
}
