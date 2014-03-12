using System;
using System.Collections.Generic;

namespace UKjiemeitao.Domain.Model
{
    public partial class SS_Category: AggregateRoot
    {

        public SS_Category()
        {
            this.ProductCollection = new List<SS_Product>();
        }

        public string Cat_ID { get; set; }
        public string LocalizedID { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public string ParentID { get; set; }

        public virtual ICollection<SS_Product> ProductCollection { get; set; }
    }
}
