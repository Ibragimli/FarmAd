using FarmAd.Domain.Entities;

using FarmAd.Application.Exceptions;
using FarmAd.Application.DTOs.Area;
using Ferma.Service.Services.Interfaces.Area;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Domain.Entities.Identity;
using FarmAd.Infrastructure.Service.User;

namespace Ferma.Service.Services.Implementations.Area
{
    public class AdminLoginServices : IAdminLoginServices
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserService _userService;
        private readonly SignInManager<AppUser> _signInManager;

        public AdminLoginServices(UserManager<AppUser> userManager, IUserService userService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _userService = userService;
            _signInManager = signInManager;
        }
        public async Task<bool> Login(AdminLoginPostDto adminLoginPostDto)
        {
            CheckValues(adminLoginPostDto);

            AppUser adminExist = await _userService.GetAsync(x => x.UserName == adminLoginPostDto.Username);

            if (adminExist != null && adminExist.IsAdmin == true)
            {
                var result = await _signInManager.PasswordSignInAsync(adminExist, adminLoginPostDto.Password, false, false);
                if (!result.Succeeded) throw new UserNotFoundException("Username və ya Passoword yanlışdır!");

                return true;
            }
            throw new UserNotFoundException("Username və ya Passoword yanlışdır!");
        }

        private void CheckValues(AdminLoginPostDto adminLoginPostDto)
        {
            if (adminLoginPostDto.Username == null)
                throw new ItemNullException("Username-i daxil edin");
            if (adminLoginPostDto.Password == null)
                throw new ItemNullException("Password-u daxil edin");
        }
        public async void Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
