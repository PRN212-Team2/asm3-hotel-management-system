using System;
using System.Collections.Generic;

namespace RepositoryLayer.Entities;

public partial class RoomInformation : BaseEntity
{
    public string RoomNumber { get; set; } = null!;

    public string? RoomDetailDescription { get; set; }

    public int? RoomMaxCapacity { get; set; }

    public int RoomTypeId { get; set; }

    public byte? RoomStatus { get; set; }

    public decimal? RoomPricePerDay { get; set; }

    public virtual RoomType RoomType { get; set; } = null!;
}
