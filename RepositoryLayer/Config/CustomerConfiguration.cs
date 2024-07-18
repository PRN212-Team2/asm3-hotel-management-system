using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Config
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");

            builder.HasIndex(e => e.EmailAddress, "UQ__Customer__49A14740C5DDFD68").IsUnique();

            builder.Property(e => e.Id).HasColumnName("CustomerID");
            builder.Property(e => e.CustomerFullName).HasMaxLength(50);
            builder.Property(e => e.EmailAddress).HasMaxLength(50);
            builder.Property(e => e.Password).HasMaxLength(50);
            builder.Property(e => e.Telephone).HasMaxLength(12);
        }
    }
}
