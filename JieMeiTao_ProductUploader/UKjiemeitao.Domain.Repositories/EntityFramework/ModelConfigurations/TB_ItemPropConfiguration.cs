using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using UKjiemeitao.Domain.Model;

namespace UKjiemeitao.Domain.Repositories.EntityFramework.ModelConfigurations
{
    public class TB_ItemPropConfiguration : EntityTypeConfiguration<TB_ItemProp>
    {
        public TB_ItemPropConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ChildTemplate)
                .HasMaxLength(2000);

            this.Property(t => t.ModifiedType)
                .IsFixedLength()
                .HasMaxLength(50);

            this.Property(t => t.Name)
                .HasMaxLength(200);

            this.Property(t => t.Status)
                .IsFixedLength()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TB_ItemProp");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Cid).HasColumnName("Cid");
            this.Property(t => t.Pid).HasColumnName("Pid");
            this.Property(t => t.ParentPid).HasColumnName("ParentPid");
            this.Property(t => t.ParentVid).HasColumnName("ParentVid");
            this.Property(t => t.ChildTemplate).HasColumnName("ChildTemplate");
            this.Property(t => t.IsAllowAlias).HasColumnName("IsAllowAlias");
            this.Property(t => t.IsColorProp).HasColumnName("IsColorProp");
            this.Property(t => t.IsEnumProp).HasColumnName("IsEnumProp");
            this.Property(t => t.IsInputProp).HasColumnName("IsInputProp");
            this.Property(t => t.IsItemProp).HasColumnName("IsItemProp");
            this.Property(t => t.IsKeyProp).HasColumnName("IsKeyProp");
            this.Property(t => t.IsSaleProp).HasColumnName("IsSaleProp");
            this.Property(t => t.ModifiedTime).HasColumnName("ModifiedTime");
            this.Property(t => t.ModifiedType).HasColumnName("ModifiedType");
            this.Property(t => t.Multi).HasColumnName("Multi");
            this.Property(t => t.Must).HasColumnName("Must");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Required).HasColumnName("Required");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
