using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKjiemeitao.Domain.Model;

namespace UKjiemeitao.Domain.Repositories.EntityFramework.ModelConfigurations
{
    public class Mapping_CategoriesMapConfiguration : EntityTypeConfiguration<Mapping_CategoriesMap>
    {
        public Mapping_CategoriesMapConfiguration()
        {
            // Properties
            this.Property(t => t.ss_cid_array)
                .HasMaxLength(100);

            this.Property(t => t.keywords)
                .HasMaxLength(50);

            this.Property(t => t.keywords_category)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Mapping_Categories");
            this.Property(t => t.tb_cid).HasColumnName("tb_cid");
            this.Property(t => t.ss_cid_array).HasColumnName("ss_cid_array");
            this.Property(t => t.keywords).HasColumnName("keywords");
            this.Property(t => t.keywords_category).HasColumnName("keywords_category");
        }
    }
}
