using System;
using System.Collections.Generic;

namespace UKjiemeitao.Domain.Model
{
    public partial class SS_Brand : AggregateRoot
    {
        public SS_Brand()
        {
            this.BrandSynonymsCollection = new List<SS_Brand_Synonyms>();
            this.ProductCollection = new List<SS_Product>();
        }

        public long Brand_ID { get; set; }
        public string Brand_Name { get; set; }
        public string Url { get; set; }

        public virtual ICollection<SS_Brand_Synonyms> BrandSynonymsCollection { get; set; }
        public virtual ICollection<SS_Product> ProductCollection { get; set; }
    }
}
