using FarmAd.Application.Exceptions;
using FarmAd.Application.Repositories.UserAuthentication;
using FarmAd.Domain.Entities.Identity;
using FarmAd.Application.Abstractions.Services.Area.UserManagers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Service.Area.UserManagers
{
    public class AdminUserManagerDeleteServices : IAdminUserManagerDeleteServices
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserAuthenticationWriteRepository _userAuthenticationWriteRepository;

        public AdminUserManagerDeleteServices(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IUserAuthenticationWriteRepository userAuthenticationWriteRepository)
        {
            _userManager = userManager;
            _userAuthenticationWriteRepository = userAuthenticationWriteRepository;
        }

        public async Task DeleteUserManager(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                throw new ItemNullException("User tapılmadı!");

            await _userManager.DeleteAsync(user);
            await _userAuthenticationWriteRepository.SaveAsync();

        }
    }
}
