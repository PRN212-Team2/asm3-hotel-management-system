using System;
using System.Collections.Generic;

namespace RepositoryLayer.Entities;

public partial class RoomType : BaseEntity
{
    public string RoomTypeName { get; set; } = null!;

    public string? TypeDescription { get; set; }

    public string? TypeNote { get; set; }
}
