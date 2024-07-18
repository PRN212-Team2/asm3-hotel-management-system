﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public enum UserRole
    {
        [EnumMember(Value = "Customer")]
        Customer,
        [EnumMember(Value = "Admin")]
        Admin
    }
}
