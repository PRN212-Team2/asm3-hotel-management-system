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
    public class BookingDetailsConfiguration : IEntityTypeConfiguration<BookingDetail>
    {
        public void Configure(EntityTypeBuilder<BookingDetail> builder)
        {
            builder.HasKey(e => new { e.BookingReservationId, e.RoomId });

            builder.ToTable("BookingDetail");

            builder.Property(e => e.BookingReservationId).HasColumnName("BookingReservationID");
            builder.Property(e => e.RoomId).HasColumnName("RoomID");
            builder.Property(e => e.ActualPrice).HasColumnType("money");
    
            builder.HasOne(bd => bd.Room)
                   .WithMany()
                   .HasForeignKey(bd => bd.RoomId)
                   .HasConstraintName("FK_BookingDetail_RoomInformation");
        }
    }
}
