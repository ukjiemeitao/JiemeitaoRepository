using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using UKjiemeitao.Domain.Model;

namespace UKjiemeitao.Domain.Repositories.EntityFramework.ModelConfigurations
{
    public class SS_TB_Brand_MappingConfiguration : EntityTypeConfiguration<SS_TB_Brand_Mapping>
    {
        public SS_TB_Brand_MappingConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.brand_id);

            // Properties
            this.Property(t => t.brand_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.brand_name)
                .HasMaxLength(100);

            this.Property(t => t.url)
                .HasMaxLength(300);

            this.Property(t => t.Name)
                .HasMaxLength(200);

            this.Property(t => t.NameAlias)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("SS_TB_Brand_Mapping");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.brand_id).HasColumnName("brand_id");
            this.Property(t => t.brand_name).HasColumnName("brand_name");
            this.Property(t => t.url).HasColumnName("url");
            this.Property(t => t.Cid).HasColumnName("Cid");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlias).HasColumnName("NameAlias");
            this.Property(t => t.Pid).HasColumnName("Pid");
            this.Property(t => t.Vid).HasColumnName("Vid");
        }
    }
}
