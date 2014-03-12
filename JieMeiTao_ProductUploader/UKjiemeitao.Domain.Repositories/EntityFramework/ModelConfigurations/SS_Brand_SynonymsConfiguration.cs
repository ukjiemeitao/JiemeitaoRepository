using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using UKjiemeitao.Domain.Model;

namespace UKjiemeitao.Domain.Repositories.EntityFramework.ModelConfigurations
{
    public class SS_Brand_SynonymsConfiguration : EntityTypeConfiguration<SS_Brand_Synonyms>
    {
        public SS_Brand_SynonymsConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Brand_ID)
                .IsRequired();

            this.Property(t => t.Synonyms_Name)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("SS_Brand_Synonyms");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Brand_ID).HasColumnName("Brand_ID");
            this.Property(t => t.Synonyms_Name).HasColumnName("Synonyms_Name");

            this.HasRequired(t => t.Brand)
                .WithMany(t => t.BrandSynonymsCollection)
                .HasForeignKey(t => t.Brand_ID);
        }
    }
}
