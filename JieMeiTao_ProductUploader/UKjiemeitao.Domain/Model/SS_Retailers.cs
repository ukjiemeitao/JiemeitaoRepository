using System;
using System.Collections.Generic;

namespace UKjiemeitao.Domain.Model
{
    public partial class SS_Retailers : AggregateRoot
    {
        public SS_Retailers()
        {
            this.ProductCollection = new List<SS_Product>();
        }

        public long Retailer_ID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Nullable<bool> DeepLinkSupport { get; set; }

        public virtual ICollection<SS_Product> ProductCollection { get; set; }
    }
}
