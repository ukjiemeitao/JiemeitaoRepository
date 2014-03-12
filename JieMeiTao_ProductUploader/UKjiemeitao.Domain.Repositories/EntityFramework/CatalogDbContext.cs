using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using UKjiemeitao.Domain.Model;
using UKjiemeitao.Domain.Repositories.EntityFramework.ModelConfigurations;

namespace UKjiemeitao.Domain.Repositories.EntityFramework
{
    public partial class CatalogDbContext : DbContext
    {
        
        /// <summary>
        /// 构造函数，初始化一个新的<c>Catalog</c>实例。
        /// </summary>
        public CatalogDbContext()
            : base("Catalog")
        {
            //解决entityframework性能问题
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Mapping_CategoriesMap> Mapping_CategoriesMap { get; set; }
        public DbSet<SS_Brand> SS_Brand { get; set; }
        public DbSet<SS_Brand_Synonyms> SS_Brand_Synonyms { get; set; }
        public DbSet<SS_Category> SS_Category { get; set; }
        public DbSet<SS_TB_Brand_Mapping> SS_TB_Brand_Mapping { get; set; }
        public DbSet<SS_Images> SS_Images { get; set; }
        public DbSet<SS_Product> SS_Product { get; set; }
        public DbSet<SS_Product_Color_Image_Mapping> SS_Product_Color_Image_Mapping { get; set; }
        public DbSet<SS_Retailers> SS_Retailers { get; set; }
        public DbSet<SS_Size> SS_Size { get; set; }
        public DbSet<TB_ItemCat> TB_ItemCat { get; set; }
        public DbSet<TB_ItemProp> TB_ItemProp { get; set; }
        public DbSet<TB_Product> TB_Product { get; set; }
        public DbSet<TB_PropValue> TB_PropValue { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new Mapping_CategoriesMapConfiguration());
            modelBuilder.Configurations.Add(new SS_BrandConfiguration());
            modelBuilder.Configurations.Add(new SS_Brand_SynonymsConfiguration());
            modelBuilder.Configurations.Add(new SS_CategoryConfiguration());
            modelBuilder.Configurations.Add(new SS_TB_Brand_MappingConfiguration());
            modelBuilder.Configurations.Add(new SS_ImagesConfiguration());
            modelBuilder.Configurations.Add(new SS_ProductConfiguration());
            modelBuilder.Configurations.Add(new SS_Product_Color_Image_MappingConfiguration());
            modelBuilder.Configurations.Add(new SS_RetailersConfiguration());
            modelBuilder.Configurations.Add(new SS_SizeConfiguration());
            modelBuilder.Configurations.Add(new TB_ItemCatConfiguration());
            modelBuilder.Configurations.Add(new TB_ItemPropConfiguration());
            modelBuilder.Configurations.Add(new TB_ProductConfiguration());
            modelBuilder.Configurations.Add(new TB_PropValueConfiguration());
        }
    }
}
