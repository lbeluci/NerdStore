using Microsoft.EntityFrameworkCore;
using NS.Core.Data;
using NS.ProductCatalog.API.Models;
using System.Linq;
using System.Threading.Tasks;

namespace NS.ProductCatalog.API.Data
{
    public class ProductCatalogContext : DbContext, IUnitOfWork
    {
        public ProductCatalogContext(DbContextOptions<ProductCatalogContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductCatalogContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}