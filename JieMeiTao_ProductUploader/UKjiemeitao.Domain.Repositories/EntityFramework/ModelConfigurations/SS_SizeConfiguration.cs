using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using UKjiemeitao.Domain.Model;

namespace UKjiemeitao.Domain.Repositories.EntityFramework.ModelConfigurations
{
    public class SS_SizeConfiguration : EntityTypeConfiguration<SS_Size>
    {
        public SS_SizeConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            this.Property(t => t.ID)
                .IsRequired();

            // Properties
            this.Property(t => t.Size_ID)
                .HasMaxLength(5);

            this.Property(t => t.Name)
                .HasMaxLength(20);

            this.Property(t => t.Cat_ID)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("SS_Size");
            this.Property(t => t.ID).HasColumnName("id");
            this.Property(t => t.Size_ID).HasColumnName("size_id");
            this.Property(t => t.Name).HasColumnName("name");
            this.Property(t => t.Cat_ID).HasColumnName("cat_id");

            HasMany(c => c.ProductCollection)
               .WithMany(p => p.SizeCollection)
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
