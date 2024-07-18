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
    public class RoomInformationConfiguration : IEntityTypeConfiguration<RoomInformation>
    {
        public void Configure(EntityTypeBuilder<RoomInformation> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("RoomInformation");

            builder.Property(e => e.Id).HasColumnName("RoomID");
            builder.Property(e => e.RoomDetailDescription).HasMaxLength(220);
            builder.Property(e => e.RoomNumber).HasMaxLength(50);
            builder.Property(e => e.RoomPricePerDay).HasColumnType("money");
            builder.Property(e => e.RoomTypeId).HasColumnName("RoomTypeID");

            builder.HasOne(r => r.RoomType).WithMany()
                .HasForeignKey(r => r.RoomTypeId)
                .HasConstraintName("FK_RoomInformation_RoomType");
        }
    }
}
