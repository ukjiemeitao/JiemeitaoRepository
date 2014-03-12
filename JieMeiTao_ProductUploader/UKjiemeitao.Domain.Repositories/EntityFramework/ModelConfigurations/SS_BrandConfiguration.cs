using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using UKjiemeitao.Domain.Model;

namespace UKjiemeitao.Domain.Repositories.EntityFramework.ModelConfigurations
{
    public class SS_BrandConfiguration : EntityTypeConfiguration<SS_Brand>
    {
        public SS_BrandConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            this.HasKey(t => t.Brand_ID);

            // Properties
            this.Property(t => t.Brand_ID)
                .IsRequired();

            this.Property(t => t.Brand_Name)
                .HasMaxLength(100);

            this.Property(t => t.Url)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("SS_Brand");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Brand_ID).HasColumnName("Brand_ID");
            this.Property(t => t.Brand_Name).HasColumnName("Brand_Name");
            this.Property(t => t.Url).HasColumnName("Url");

        }
    }
}
