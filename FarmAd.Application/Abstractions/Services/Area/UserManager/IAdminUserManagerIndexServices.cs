using FarmAd.Domain.Entities;
using FarmAd.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.Area.UserManagers
{
    public interface IAdminUserManagerIndexServices
    {
        public IQueryable<AppUser> GetUserManager(string name);
        public IQueryable<AppUser> GetAdminManager(string name);

    }
}
