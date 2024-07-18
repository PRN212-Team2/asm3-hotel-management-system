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
    public class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            builder.ToTable("RoomType");

            builder.Property(e => e.Id).HasColumnName("RoomTypeID");
            builder.Property(e => e.RoomTypeName).HasMaxLength(50);
            builder.Property(e => e.TypeDescription).HasMaxLength(250);
            builder.Property(e => e.TypeNote).HasMaxLength(250);
        }
    }
}
