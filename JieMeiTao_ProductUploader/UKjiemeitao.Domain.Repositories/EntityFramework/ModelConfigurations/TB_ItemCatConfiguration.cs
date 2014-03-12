using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using UKjiemeitao.Domain.Model;

namespace UKjiemeitao.Domain.Repositories.EntityFramework.ModelConfigurations
{
    public class TB_ItemCatConfiguration : EntityTypeConfiguration<TB_ItemCat>
    {
        public TB_ItemCatConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(200);

            this.Property(t => t.ModifiedType)
                .IsFixedLength()
                .HasMaxLength(50);

            this.Property(t => t.Status)
                .IsFixedLength()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TB_ItemCat");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Cid).HasColumnName("Cid");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsParent).HasColumnName("IsParent");
            this.Property(t => t.ModifiedTime).HasColumnName("ModifiedTime");
            this.Property(t => t.ModifiedType).HasColumnName("ModifiedType");
            this.Property(t => t.ParentCid).HasColumnName("ParentCid");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
