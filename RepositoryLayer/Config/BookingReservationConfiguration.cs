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
    public class BookingReservationConfiguration : IEntityTypeConfiguration<BookingReservation>
    {
        public void Configure(EntityTypeBuilder<BookingReservation> builder)
        {
            builder.ToTable("BookingReservation");

            builder.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("BookingReservationID");
            builder.Property(e => e.CustomerId).HasColumnName("CustomerID");
            builder.Property(e => e.TotalPrice).HasColumnType("money");

            builder.HasMany(br => br.BookingDetails)
                .WithOne()
                .HasForeignKey(bd => bd.BookingReservationId)
                .HasConstraintName("FK_BookingDetail_BookingReservation");

            builder.HasOne(br => br.Customer)
                .WithMany()
                .HasForeignKey(br => br.CustomerId)
                .HasConstraintName("FK_BookingReservation_Customer");
        }
    }
}
