using FarmAd.Domain.Entities;
using FarmAd.Application.DTOs.Area;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.Area.UserManagers
{
    public interface IAdminUserManagerCreateServices
    {
        Task CreateUserManager(UserManagerCreateDto UserManagerCreateDto);
        Task<List<IdentityRole>> GetRoles();

    }
}
