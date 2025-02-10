using FarmAd.Application.DTOs.Area;
using FarmAd.Application.Exceptions;
using FarmAd.Application.Repositories.UserAuthentication;
using FarmAd.Domain.Entities.Identity;
using FarmAd.Application.Abstractions.Services.Area.UserManagers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Service.Area.UserManagers
{
    public class AdminUserManagerEditServices : IAdminUserManagerEditServices
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserAuthenticationWriteRepository userAuthenticationWriteRepository;

        public AdminUserManagerEditServices(UserManager<AppUser> UserManager, RoleManager<IdentityRole> roleManager,IUserAuthenticationWriteRepository userAuthenticationWriteRepository)
        {
            _userManager = UserManager;
            _roleManager = roleManager;
            this.userAuthenticationWriteRepository = userAuthenticationWriteRepository;
        }

        public async Task EditUserManager(UserManagerEditDto userManagerEditDto)
        {
            bool checkBool = false;
            await DtoCheck(userManagerEditDto);

            var userExits = await GetUserManager(userManagerEditDto.Id);
            var newRole = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == userManagerEditDto.RoleId);

            if (!await _userManager.IsInRoleAsync(userExits, newRole.Name))
            {
                //await _userManager.RemoveFromRoleAsync(userExits, userExits.RoleName);
                await _userManager.AddToRoleAsync(userExits, newRole.Name);
                checkBool = true;
            }

            if (userManagerEditDto.Password != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(userExits);
                var errors = await _userManager.ResetPasswordAsync(userExits, token, userManagerEditDto.Password);
                if (!errors.Succeeded)
                {
                    foreach (var error in errors.Errors)
                    {
                        throw new UserPasswordResetException(error.Description);
                    }
                }
                checkBool = true;
            }
            //if (userExits.RoleName != newRole.Name)
            //{
            //    userExits.RoleName = newRole.Name;
            //    checkBool = true;
            //}
            if (userExits.UserName != userManagerEditDto.Username)
            {
                userExits.UserName = userManagerEditDto.Username;
                userExits.NormalizedUserName = userManagerEditDto.Username.ToUpper();
                checkBool = true;
            }
            if (userExits.Fullname != userManagerEditDto.Fullname)
            {
                userExits.Fullname = userManagerEditDto.Fullname;
                checkBool = true;
            }
            if (userExits.IsAdmin != userManagerEditDto.IsAdmin)
            {
                userExits.IsAdmin = userManagerEditDto.IsAdmin;
                checkBool = true;
            }
            if (checkBool)
                await userAuthenticationWriteRepository.SaveAsync();
        }
        public async Task<AppUser> GetUserManager(string Id)
        {
            var UserExist = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == Id);

            return UserExist;
        }

      
        //public async Task<string> RoleName(string id)
        //{
        //    var roleName = await GetUserManager(id);
        //    return roleName.RoleName;
        //}
        private async Task DtoCheck(UserManagerEditDto userManagerEditDto)
        {
            var UserExist = await _userManager.FindByNameAsync(userManagerEditDto.Username);

            if (UserExist != null && UserExist.Id != userManagerEditDto.Id)
                throw new ItemAlreadyException("Username database-də mövcüddur!");

        }
    }
}
