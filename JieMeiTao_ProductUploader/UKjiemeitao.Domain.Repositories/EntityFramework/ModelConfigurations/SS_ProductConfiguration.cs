using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using UKjiemeitao.Domain.Model;

namespace UKjiemeitao.Domain.Repositories.EntityFramework.ModelConfigurations
{
    public class SS_ProductConfiguration : EntityTypeConfiguration<SS_Product>
    {
        public SS_ProductConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.ID);
            this.HasKey(t => t.Product_ID);

            this.Property(t => t.Product_ID)
                .IsRequired();
            
            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(200);

            this.Property(t => t.Currency)
                .HasMaxLength(10);

            this.Property(t => t.Price_Label)
                .HasMaxLength(20);

            this.Property(t => t.Sale_Price_Label)
                .HasMaxLength(20);

            this.Property(t => t.Locale)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(1000);

            this.Property(t => t.Click_Url)
                .HasMaxLength(300);

            this.Property(t => t.Page_Url)
                .HasMaxLength(300);

            this.Property(t => t.Image_ID)
                .HasMaxLength(50);

            this.Property(t => t.Chinese_Name)
                .HasMaxLength(200);

            this.Property(t => t.Chinese_Description)
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("SS_Product");
            this.Property(t => t.ID).HasColumnName("id");
            this.Property(t => t.Product_ID).HasColumnName("product_id");
            this.Property(t => t.Name).HasColumnName("name");
            this.Property(t => t.Currency).HasColumnName("currency");
            this.Property(t => t.Price).HasColumnName("price");
            this.Property(t => t.Price_Label).HasColumnName("price_label");
            this.Property(t => t.Sale_Price).HasColumnName("sale_price");
            this.Property(t => t.Sale_Price_Label).HasColumnName("sale_price_label");
            this.Property(t => t.In_Stock).HasColumnName("in_stock");
            this.Property(t => t.Retailer_ID).HasColumnName("retailer_id");
            this.Property(t => t.Locale).HasColumnName("locale");
            this.Property(t => t.Description).HasColumnName("description");
            this.Property(t => t.Brand_ID).HasColumnName("brand_id");
            this.Property(t => t.Click_Url).HasColumnName("click_url");
            this.Property(t => t.Page_Url).HasColumnName("page_url");
            this.Property(t => t.Image_ID).HasColumnName("image_id");
            this.Property(t => t.Chinese_Name).HasColumnName("chinese_name");
            this.Property(t => t.Chinese_Description).HasColumnName("chinese_description");
            this.Property(t => t.IsTranslate).HasColumnName("IsTranslate");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");

            this.HasRequired(t => t.Brand)
                .WithMany(t => t.ProductCollection)
                .HasForeignKey(t => t.Brand_ID);

            this.HasRequired(t => t.Retailer)
               .WithMany(t=>t.ProductCollection)
               .HasForeignKey(t => t.Retailer_ID);

            HasMany(p => p.CategoryCollection)
               .WithMany(c => c.ProductCollection)
               .Map(r =>
               {
                   r.ToTable("SS_Product_Category_Mapping");
                   r.MapLeftKey("Cat_ID");
                   r.MapRightKey("Product_ID");
               }
           );

            HasMany(c => c.SizeCollection)
               .WithMany(p => p.ProductCollection)
               .Map(r =>
               {
                   r.ToTable("SS_Product_Size_Mapping");
                   r.MapLeftKey("Product_ID");
                   r.MapRightKey("ID");
               }
           );
        }
    }
}
