using System;
using System.Collections.Generic;

namespace UKjiemeitao.Domain.Model
{
    public partial class SS_Color : AggregateRoot
    {
        public Nullable<int> Color_ID { get; set; }
        public string Color_Name { get; set; }
        public string Url { get; set; }

        
    }
}
