using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using UKjiemeitao.Domain.Model;

namespace UKjiemeitao.Domain.Repositories.EntityFramework.ModelConfigurations
{
    public class TB_ProductConfiguration : EntityTypeConfiguration<TB_Product>
    {
        public TB_ProductConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(30);

            this.Property(t => t.StuffStatus)
                .HasMaxLength(50);

            this.Property(t => t.PropertyAlias)
                .HasMaxLength(511);

            this.Property(t => t.Props)
                .HasMaxLength(549);

            this.Property(t => t.PicPath)
                .IsFixedLength()
                .HasMaxLength(1000);

            this.Property(t => t.Type)
                .HasMaxLength(50);

            this.Property(t => t.LocationState)
                .HasMaxLength(100);

            this.Property(t => t.LocationCity)
                .HasMaxLength(100);

            this.Property(t => t.ApproveStatus)
                .HasMaxLength(50);

            this.Property(t => t.FreightPayer)
                .HasMaxLength(50);

            this.Property(t => t.SellerCids)
                .HasMaxLength(200);

            this.Property(t => t.InputPids)
                .HasMaxLength(200);

            this.Property(t => t.InputStr)
                .HasMaxLength(200);

            this.Property(t => t.Increment)
                .HasMaxLength(50);

            this.Property(t => t.ImgFilePath)
                .HasMaxLength(256);

            this.Property(t => t.SkuProperties)
                .HasMaxLength(4000);

            this.Property(t => t.SkuQuantities)
                .HasMaxLength(4000);

            this.Property(t => t.SkuPrices)
                .HasMaxLength(4000);

            this.Property(t => t.SkuOuterIds)
                .HasMaxLength(4000);

            this.Property(t => t.Lang)
                .HasMaxLength(50);

            this.Property(t => t.OuterId)
                .HasMaxLength(512);

            this.Property(t => t.ChangeProp)
                .HasMaxLength(2000);

            this.Property(t => t.DescModules)
                .HasMaxLength(2000);

            this.Property(t => t.GlobalStockType)
                .HasMaxLength(50);

            this.Property(t => t.GlobalStockCountry)
                .HasMaxLength(200);

            this.Property(t => t.EmptyFields)
                .HasMaxLength(4000);

            // Table & Column Mappings
            this.ToTable("TB_Product");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Desc).HasColumnName("Desc");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.StuffStatus).HasColumnName("StuffStatus");
            this.Property(t => t.Cid).HasColumnName("Cid");
            this.Property(t => t.PropertyAlias).HasColumnName("PropertyAlias");
            this.Property(t => t.Props).HasColumnName("Props");
            this.Property(t => t.PicPath).HasColumnName("PicPath");
            this.Property(t => t.Num).HasColumnName("Num");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.LocationState).HasColumnName("LocationState");
            this.Property(t => t.LocationCity).HasColumnName("LocationCity");
            this.Property(t => t.ApproveStatus).HasColumnName("ApproveStatus");
            this.Property(t => t.FreightPayer).HasColumnName("FreightPayer");
            this.Property(t => t.ValidThru).HasColumnName("ValidThru");
            this.Property(t => t.HasInvoice).HasColumnName("HasInvoice");
            this.Property(t => t.HasWarranty).HasColumnName("HasWarranty");
            this.Property(t => t.HasShowcase).HasColumnName("HasShowcase");
            this.Property(t => t.SellerCids).HasColumnName("SellerCids");
            this.Property(t => t.InputPids).HasColumnName("InputPids");
            this.Property(t => t.InputStr).HasColumnName("InputStr");
            this.Property(t => t.HasDiscount).HasColumnName("HasDiscount");
            this.Property(t => t.PostFee).HasColumnName("PostFee");
            this.Property(t => t.ExpressFee).HasColumnName("ExpressFee");
            this.Property(t => t.EmsFee).HasColumnName("EmsFee");
            this.Property(t => t.ListTime).HasColumnName("ListTime");
            this.Property(t => t.Increment).HasColumnName("Increment");
            this.Property(t => t.ImgFilePath).HasColumnName("ImgFilePath");
            this.Property(t => t.PostageId).HasColumnName("PostageId");
            this.Property(t => t.AuctionPoint).HasColumnName("AuctionPoint");
            this.Property(t => t.SkuProperties).HasColumnName("SkuProperties");
            this.Property(t => t.SkuQuantities).HasColumnName("SkuQuantities");
            this.Property(t => t.SkuPrices).HasColumnName("SkuPrices");
            this.Property(t => t.SkuOuterIds).HasColumnName("SkuOuterIds");
            this.Property(t => t.Lang).HasColumnName("Lang");
            this.Property(t => t.OuterId).HasColumnName("OuterId");
            this.Property(t => t.IsTaobao).HasColumnName("IsTaobao");
            this.Property(t => t.IsEx).HasColumnName("IsEx");
            this.Property(t => t.Is3D).HasColumnName("Is3D");
            this.Property(t => t.SellPromise).HasColumnName("SellPromise");
            this.Property(t => t.AfterSaleId).HasColumnName("AfterSaleId");
            this.Property(t => t.CodPostageId).HasColumnName("CodPostageId");
            this.Property(t => t.IsLightningConsignment).HasColumnName("IsLightningConsignment");
            this.Property(t => t.Weight).HasColumnName("Weight");
            this.Property(t => t.IsXinpin).HasColumnName("IsXinpin");
            this.Property(t => t.SubStock).HasColumnName("SubStock");
            this.Property(t => t.ItemSize).HasColumnName("ItemSize");
            this.Property(t => t.ItemWeight).HasColumnName("ItemWeight");
            this.Property(t => t.ChangeProp).HasColumnName("ChangeProp");
            this.Property(t => t.DescModules).HasColumnName("DescModules");
            this.Property(t => t.GlobalStockType).HasColumnName("GlobalStockType");
            this.Property(t => t.GlobalStockCountry).HasColumnName("GlobalStockCountry");
            this.Property(t => t.NumIid).HasColumnName("NumIid");
            this.Property(t => t.IsReplaceSku).HasColumnName("IsReplaceSku");
            this.Property(t => t.EmptyFields).HasColumnName("EmptyFields");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
