using FarmAd.Domain.Entities;
using FarmAd.Application.DTOs.Area;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Domain.Entities.Identity;

namespace FarmAd.Application.Abstractions.Services.Area.UserManagers
{
    public interface IAdminUserManagerEditServices
    {
        public Task<AppUser> GetUserManager(string Id);
        public Task EditUserManager(UserManagerEditDto UserManagerEditDto);
        //public Task<string> RoleName(string id);

    }
}
