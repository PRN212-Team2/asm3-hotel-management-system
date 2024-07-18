using System;
using System.Collections.Generic;

namespace RepositoryLayer.Entities;

public partial class Customer : BaseEntity
{
    public string? CustomerFullName { get; set; }

    public string? Telephone { get; set; }

    public string EmailAddress { get; set; } = null!;

    public DateOnly CustomerBirthday { get; set; }

    public byte? CustomerStatus { get; set; }

    public string? Password { get; set; } 
}
