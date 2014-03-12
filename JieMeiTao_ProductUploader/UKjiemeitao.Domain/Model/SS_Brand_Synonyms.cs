using System;
using System.Collections.Generic;

namespace UKjiemeitao.Domain.Model
{
    public partial class SS_Brand_Synonyms : AggregateRoot
    {
    
        public long Brand_ID { get; set; }
        public string Synonyms_Name { get; set; }

        
        public virtual SS_Brand Brand { get; set; }
    }
}
