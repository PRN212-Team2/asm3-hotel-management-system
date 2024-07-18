﻿using BusinessServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServiceLayer.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> LoginAsync(string email, string password);
        Task<UserDTO> GetUserByEmailAsync(string email);
        Task<bool> CheckEmailExisted(string email);
        Task<bool> CheckPhoneExisted(string phoneNumber);
    }
}
