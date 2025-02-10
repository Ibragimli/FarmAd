using FarmAd.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.Area.RoleManagers
{
    public interface IAdminRoleManagerIndexServices
    {
        public IQueryable<IdentityRole> GetRoleManager(string name);
    }
}
