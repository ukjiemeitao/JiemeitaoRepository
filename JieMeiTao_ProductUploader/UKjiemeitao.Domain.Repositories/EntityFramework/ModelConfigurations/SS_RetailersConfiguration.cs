using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using UKjiemeitao.Domain.Model;

namespace UKjiemeitao.Domain.Repositories.EntityFramework.ModelConfigurations
{
    public class SS_RetailersConfiguration : EntityTypeConfiguration<SS_Retailers>
    {
        public SS_RetailersConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.ID);
            this.HasKey(t => t.Retailer_ID);

            this.Property(t => t.Retailer_ID)
                .IsRequired();

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(80);

            this.Property(t => t.Url)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("SS_Retailers");
            this.Property(t => t.ID).HasColumnName("id");
            this.Property(t => t.Retailer_ID).HasColumnName("retailer_id");
            this.Property(t => t.Name).HasColumnName("name");
            this.Property(t => t.Url).HasColumnName("url");
            this.Property(t => t.DeepLinkSupport).HasColumnName("deeplinkSupport");

           
        }
    }
}
