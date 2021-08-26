using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NS.ProductCatalog.API.Models;

namespace NS.ProductCatalog.API.Data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired().HasColumnType("varchar(250)");

            builder.Property(p => p.Description).IsRequired().HasColumnType("varchar(250)");

            builder.Property(p => p.Image).IsRequired().HasColumnType("varchar(250)");

            builder.ToTable("Products");
        }
    }
}