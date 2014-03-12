using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using UKjiemeitao.Domain.Model;

namespace UKjiemeitao.Domain.Repositories.EntityFramework.ModelConfigurations
{
    public class TB_PropValueConfiguration : EntityTypeConfiguration<TB_PropValue>
    {
        public TB_PropValueConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ModifiedType)
                .IsFixedLength()
                .HasMaxLength(50);

            this.Property(t => t.Name)
                .HasMaxLength(200);

            this.Property(t => t.NameAlias)
                .HasMaxLength(200);

            this.Property(t => t.PropName)
                .HasMaxLength(200);

            this.Property(t => t.Status)
                .IsFixedLength()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TB_PropValue");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Cid).HasColumnName("Cid");
            this.Property(t => t.IsParent).HasColumnName("IsParent");
            this.Property(t => t.ModifiedTime).HasColumnName("ModifiedTime");
            this.Property(t => t.ModifiedType).HasColumnName("ModifiedType");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlias).HasColumnName("NameAlias");
            this.Property(t => t.Pid).HasColumnName("Pid");
            this.Property(t => t.PropName).HasColumnName("PropName");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Vid).HasColumnName("Vid");
        }
    }
}
