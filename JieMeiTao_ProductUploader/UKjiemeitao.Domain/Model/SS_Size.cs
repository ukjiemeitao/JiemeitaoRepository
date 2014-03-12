using System;
using System.Collections.Generic;

namespace UKjiemeitao.Domain.Model
{

    public partial class SS_Size : AggregateRoot
    {
        public SS_Size()
        {
            this.ProductCollection = new List<SS_Product>();
        }

        public string Size_ID { get; set; }
        public string Name { get; set; }
        public string Cat_ID { get; set; }
        public virtual ICollection<SS_Product> ProductCollection { get; set; }
    }
}
