using System;
using System.Collections.Generic;

namespace UKjiemeitao.Domain.Model
{
    public partial class SS_Product : AggregateRoot
    {
        public SS_Product()
        {
            this.CategoryCollection = new List<SS_Category>();
            this.SizeCollection = new List<SS_Size>();
            this.ColorCollection = new List<SS_Product_Color_Image_Mapping>();
        }
        public long Product_ID { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Price_Label { get; set; }
        public Nullable<decimal> Sale_Price { get; set; }
        public string Sale_Price_Label { get; set; }
        public Nullable<bool> In_Stock { get; set; }
        public Nullable<long> Retailer_ID { get; set; }
        public string Locale { get; set; }
        public string Description { get; set; }
        public long Brand_ID { get; set; }
        public string Click_Url { get; set; }
        public string Page_Url { get; set; }
        public string Image_ID { get; set; }
        public string Chinese_Name { get; set; }
        public string Chinese_Description { get; set; }
        public Nullable<bool> IsTranslate { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }

        public virtual SS_Brand Brand { get; set; }
        public virtual SS_Retailers Retailer { get; set; }
        public virtual ICollection<SS_Category> CategoryCollection { get; set; }
        public virtual ICollection<SS_Size> SizeCollection { get; set; }
        public virtual ICollection<SS_Product_Color_Image_Mapping> ColorCollection { get; set; }
        
    }
}
