using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using UKjiemeitao.Domain.Model;

namespace UKjiemeitao.Domain.Repositories.EntityFramework.ModelConfigurations
{
    public class SS_Product_Color_Image_MappingConfiguration : EntityTypeConfiguration<SS_Product_Color_Image_Mapping>
    {
        public SS_Product_Color_Image_MappingConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.ID);
            this.HasKey(t => t.Image_ID);

            // Properties
            this.Property(t => t.Color_Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Image_ID)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SS_Product_Color_Image_Mapping");
            this.Property(t => t.ID).HasColumnName("id");
            this.Property(t => t.Product_ID).HasColumnName("product_id");
            this.Property(t => t.Color_Name).HasColumnName("color_name");
            this.Property(t => t.Image_ID).HasColumnName("image_id");

            this.HasRequired(t => t.Product)
                .WithMany(t => t.ColorCollection)
                .HasForeignKey(t => t.Product_ID);
        }
    }
}
