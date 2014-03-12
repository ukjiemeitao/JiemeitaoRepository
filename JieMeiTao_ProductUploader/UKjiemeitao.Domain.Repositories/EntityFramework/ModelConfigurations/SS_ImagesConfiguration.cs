using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using UKjiemeitao.Domain.Model;

namespace UKjiemeitao.Domain.Repositories.EntityFramework.ModelConfigurations
{
    public class SS_ImagesConfiguration : EntityTypeConfiguration<SS_Images>
    {
        public SS_ImagesConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Image_ID)
                .HasMaxLength(50);

            this.Property(t => t.Size_Name)
                .HasMaxLength(30);

            this.Property(t => t.Url)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("SS_Images");
            this.Property(t => t.ID).HasColumnName("id");
            this.Property(t => t.Image_ID).HasColumnName("image_id");
            this.Property(t => t.Size_Name).HasColumnName("size_name");
            this.Property(t => t.Width).HasColumnName("width");
            this.Property(t => t.Height).HasColumnName("height");
            this.Property(t => t.Url).HasColumnName("url");

            this.HasRequired(t => t.Color)
               .WithMany(t => t.ImageCollection)
               .HasForeignKey(t => t.Image_ID);
           
        }
    }
}
