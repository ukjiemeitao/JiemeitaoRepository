using System;
using System.Collections.Generic;


namespace UKjiemeitao.Domain.Model
{
    public partial class SS_Product_Color_Image_Mapping : AggregateRoot
    {
        public SS_Product_Color_Image_Mapping()
        {
            this.ImageCollection = new List<SS_Images>();

        }


        public long Product_ID { get; set; }
        public string Color_Name { get; set; }
        public string Image_ID { get; set; }

        public virtual SS_Product Product { get; set; }

        public virtual ICollection<SS_Images> ImageCollection { get; set; }
    }
}
