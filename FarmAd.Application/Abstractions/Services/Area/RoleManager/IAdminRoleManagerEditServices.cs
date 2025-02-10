using FarmAd.Domain.Entities;
using FarmAd.Application.DTOs.Area;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.Area.RoleManagers
{
    public interface IAdminRoleManagerEditServices
    {
        public Task<IdentityRole> GetRoleManager(string Id);
        public Task EditRoleManager(RoleManagerEditDto roleManagerEditDto);

    }
}
