using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using UKjiemeitao.Domain.Model;

namespace UKjiemeitao.Domain.Repositories.EntityFramework.ModelConfigurations
{
    public class SS_CategoryConfiguration : EntityTypeConfiguration<SS_Category>
    {
        public SS_CategoryConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.ID);
            this.HasKey(t => t.Cat_ID);

            // Properties
            this.Property(t => t.Cat_ID)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.LocalizedID)
                .HasMaxLength(100);

            this.Property(t => t.ShortName)
                .HasMaxLength(80);

            this.Property(t => t.Name)
                .HasMaxLength(50);

            this.Property(t => t.ParentID)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("SS_Category");
            this.Property(t => t.ID).HasColumnName("id");
            this.Property(t => t.Cat_ID).HasColumnName("cat_id");
            this.Property(t => t.LocalizedID).HasColumnName("localizedid");
            this.Property(t => t.ShortName).HasColumnName("shortname");
            this.Property(t => t.Name).HasColumnName("name");
            this.Property(t => t.ParentID).HasColumnName("parentid");

            HasMany(c => c.ProductCollection)
               .WithMany(p => p.CategoryCollection)
               .Map(r =>
               {
                   r.ToTable("SS_Product_Category_Mapping");
                   r.MapLeftKey("Cat_ID");
                   r.MapRightKey("Product_ID");
               }
           );
        }
    }
}
