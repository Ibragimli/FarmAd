using FarmAd.Application.Abstractions.Services.Area.UserManagers;
using FarmAd.Application.DTOs.Area;
using FarmAd.Application.Exceptions;
using FarmAd.Application.Repositories.UserAuthentication;
using FarmAd.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Service.Area.UserManager
{
    public class AdminUserManagerCreateServices : IAdminUserManagerCreateServices
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserAuthenticationWriteRepository _userAuthenticationWriteRepository;

        public AdminUserManagerCreateServices(UserManager<AppUser> UserManager, RoleManager<IdentityRole> roleManager, IUserAuthenticationWriteRepository userAuthenticationWriteRepository)
        {
            _userManager = UserManager;
            _roleManager = roleManager;
            _userAuthenticationWriteRepository = userAuthenticationWriteRepository;
        }


        public async Task CreateUserManager(UserManagerCreateDto userManagerCreateDto)
        {
            await DtoCheck(userManagerCreateDto);
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id == userManagerCreateDto.RoleId);
            AppUser newAdmin = new AppUser { Name = userManagerCreateDto.Name, IsAdmin = userManagerCreateDto.IsAdmin, UserName = userManagerCreateDto.Username/*, rol = role.Name */};
            var admin = await _userManager.CreateAsync(newAdmin, userManagerCreateDto.Password);
            if (!admin.Succeeded)
                throw new ValueFormatExpception(admin.Errors.FirstOrDefault().Description);
            var resultRole = await _userManager.AddToRoleAsync(newAdmin, role.Name);
            if (!resultRole.Succeeded)
                throw new ValueFormatExpception(resultRole.Errors.FirstOrDefault().Description);
            await _userAuthenticationWriteRepository.SaveAsync();
        }

        public async Task<List<IdentityRole>> GetRoles()
        {
            var roles = await _roleManager.Roles.Where(x => x.Name != null).ToListAsync();
            return roles;
        }

        private async Task DtoCheck(UserManagerCreateDto userManagerCreateDto)
        {
            if (userManagerCreateDto.Username == null)
                throw new ItemNullException("Username  qeyd edin!");
            if (userManagerCreateDto.Password == null)
                throw new ItemNullException("Password  qeyd edin!");
            if (userManagerCreateDto.Name == null)
                throw new ItemNullException("Ad  qeyd edin!");
            if (userManagerCreateDto.RoleId == null)
                throw new ItemNullException("Role  qeyd edin!");

            if (userManagerCreateDto.Username?.Length < 4)
                throw new ValueFormatExpception("Username uzunluğu minimum 4 ola bilər");

            var UserExist = _userManager.FindByNameAsync(userManagerCreateDto.Username);

            if (UserExist.Result != null)
                throw new ItemAlreadyException("Username database-də mövcüddur!");
        }
    }
}
