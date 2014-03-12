using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using UKjiemeitao.Domain.Model;

namespace UKjiemeitao.Domain.Repositories.EntityFramework.ModelConfigurations
{
    public class SS_ColorConfiguration : EntityTypeConfiguration<SS_Color>
    {
        public SS_ColorConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Color_Name)
                .HasMaxLength(50);

            this.Property(t => t.Url)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("SS_Color");
            this.Property(t => t.ID).HasColumnName("id");
            this.Property(t => t.Color_ID).HasColumnName("color_id");
            this.Property(t => t.Color_Name).HasColumnName("color_name");
            this.Property(t => t.Url).HasColumnName("url");
        }
    }
}
