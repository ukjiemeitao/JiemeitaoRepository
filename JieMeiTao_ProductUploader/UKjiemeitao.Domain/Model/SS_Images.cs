using System;
using System.Collections.Generic;

namespace UKjiemeitao.Domain.Model
{
    public partial class SS_Images : AggregateRoot
    {
        public string Image_ID { get; set; }
        public string Size_Name { get; set; }
        public Nullable<int> Width { get; set; }
        public Nullable<int> Height { get; set; }
        public string Url { get; set; }

        public virtual SS_Product_Color_Image_Mapping Color { get; set; }
    }
}
