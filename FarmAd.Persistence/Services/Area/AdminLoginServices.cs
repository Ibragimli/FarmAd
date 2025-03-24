using FarmAd.Domain.Entities;

using FarmAd.Application.Exceptions;
using FarmAd.Application.DTOs.Area;
using FarmAd.Application.Abstractions.Services.Area;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Domain.Entities.Identity;
using FarmAd.Infrastructure.Service.User;
using FarmAd.Application.DTOs;
using FarmAd.Application.Abstractions.Tokens;

namespace FarmAd.Persistence.Service.Area
{
    public class AdminLoginServices : IAdminLoginServices
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly IUserService _userService;
        private readonly SignInManager<AppUser> _signInManager;

        public AdminLoginServices(UserManager<AppUser> userManager, ITokenHandler tokenHandler, IUserService userService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _userService = userService;
            _signInManager = signInManager;
        }
        public async Task<Token> Login(AdminLoginPostDto adminLoginPostDto)
        {
            CheckValues(adminLoginPostDto);

            AppUser adminExist = await _userService.GetAsync(x => x.UserName == adminLoginPostDto.Username);

            if (adminExist == null || adminExist.IsAdmin == false)
                throw new UserNotFoundException("Username və ya Password yanlışdır!");

            var result = await _signInManager.PasswordSignInAsync(adminExist.UserName, adminLoginPostDto.Password, false, false);

            if (!result.Succeeded)
                throw new UserNotFoundException("Username və ya Password yanlışdır!");

            // Token oluştur ve return et
            Token token = _tokenHandler.CreateAccesToken(600, adminExist);
            return token;
        }

        private void CheckValues(AdminLoginPostDto adminLoginPostDto)
        {
            if (string.IsNullOrWhiteSpace(adminLoginPostDto.Username))
                throw new ItemNullException("Username-i daxil edin");

            if (string.IsNullOrWhiteSpace(adminLoginPostDto.Password))
                throw new ItemNullException("Password-u daxil edin");
        }


    }
}
