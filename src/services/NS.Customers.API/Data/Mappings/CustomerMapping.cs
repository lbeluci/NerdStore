using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NS.Core.DomainObjects;
using NS.Customers.API.Models;

namespace NS.Customers.API.Data.Mappings
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).IsRequired().HasColumnType("varchar(200)");

            builder.OwnsOne(c => c.Cpf, ot => 
            {
                ot.Property(c => c.Number).IsRequired().HasMaxLength(Cpf.MaxLength).HasColumnName("Cpf").HasColumnType($"varchar({Cpf.MaxLength})");
            });

            builder.OwnsOne(c => c.Email, ot =>
            {
                ot.Property(c => c.Address).IsRequired().HasColumnName("Email").HasColumnType($"varchar({Email.MaxLength})");
            });


        }
    }
}