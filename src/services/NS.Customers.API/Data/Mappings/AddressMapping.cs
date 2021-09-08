﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NS.Customers.API.Models;

namespace NS.Customers.API.Data.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Patio).IsRequired().HasColumnType("varchar(200)");

            builder.Property(c => c.Number).IsRequired().HasColumnType("varchar(50)");

            builder.Property(c => c.Complement).IsRequired().HasColumnType("varchar(250)");

            builder.Property(c => c.District).IsRequired().HasColumnType("varchar(100)");

            builder.Property(c => c.ZipCode).IsRequired().HasColumnType("varchar(50)");

            builder.Property(c => c.City).IsRequired().HasColumnType("varchar(100)");

            builder.Property(c => c.State).IsRequired().HasColumnType("varchar(50)");

            builder.ToTable("Addresses");
        }
    }
}