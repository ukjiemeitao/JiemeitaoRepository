using System;
using System.Collections.Generic;

namespace UKjiemeitao.Domain.Model
{
    public partial class TB_Product : AggregateRoot
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string StuffStatus { get; set; }
        public Nullable<long> Cid { get; set; }
        public string PropertyAlias { get; set; }
        public string Props { get; set; }
        public string PicPath { get; set; }
        public Nullable<long> Num { get; set; }
        public string Type { get; set; }
        public string LocationState { get; set; }
        public string LocationCity { get; set; }
        public string ApproveStatus { get; set; }
        public string FreightPayer { get; set; }
        public Nullable<long> ValidThru { get; set; }
        public Nullable<bool> HasInvoice { get; set; }
        public Nullable<bool> HasWarranty { get; set; }
        public Nullable<bool> HasShowcase { get; set; }
        public string SellerCids { get; set; }
        public string InputPids { get; set; }
        public string InputStr { get; set; }
        public Nullable<bool> HasDiscount { get; set; }
        public Nullable<decimal> PostFee { get; set; }
        public Nullable<decimal> ExpressFee { get; set; }
        public Nullable<decimal> EmsFee { get; set; }
        public Nullable<System.DateTime> ListTime { get; set; }
        public string Increment { get; set; }
        public string ImgFilePath { get; set; }
        public Nullable<long> PostageId { get; set; }
        public Nullable<long> AuctionPoint { get; set; }
        public string SkuProperties { get; set; }
        public string SkuQuantities { get; set; }
        public string SkuPrices { get; set; }
        public string SkuOuterIds { get; set; }
        public string Lang { get; set; }
        public string OuterId { get; set; }
        public Nullable<bool> IsTaobao { get; set; }
        public Nullable<bool> IsEx { get; set; }
        public Nullable<bool> Is3D { get; set; }
        public Nullable<bool> SellPromise { get; set; }
        public Nullable<long> AfterSaleId { get; set; }
        public Nullable<long> CodPostageId { get; set; }
        public Nullable<bool> IsLightningConsignment { get; set; }
        public Nullable<long> Weight { get; set; }
        public Nullable<bool> IsXinpin { get; set; }
        public Nullable<long> SubStock { get; set; }
        public Nullable<decimal> ItemSize { get; set; }
        public Nullable<decimal> ItemWeight { get; set; }
        public string ChangeProp { get; set; }
        public string DescModules { get; set; }
        public string GlobalStockType { get; set; }
        public string GlobalStockCountry { get; set; }
        public Nullable<long> NumIid { get; set; }
        public Nullable<bool> IsReplaceSku { get; set; }
        public string EmptyFields { get; set; }
        public Nullable<int> Status { get; set; }
    }
}
