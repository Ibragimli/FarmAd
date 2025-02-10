using FarmAd.Domain.Entities;

using FarmAd.Application.Exceptions;
using FarmAd.Application.Abstractions.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Domain.Entities.Identity;
using FarmAd.Application.Repositories.UserAuthentication;

namespace FarmAd.Persistence.Services.User
{
    public class ProfileLoginServices : IProfileLoginServices
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUserAuthenticationWriteRepository _userAuthenticationWriteRepository;
        private readonly IUserAuthenticationReadRepository _userAuthenticationReadRepository;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public ProfileLoginServices(IHttpContextAccessor httpContext, IUserAuthenticationWriteRepository userAuthenticationWriteRepository, IUserAuthenticationReadRepository userAuthenticationReadRepository, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _httpContext = httpContext;
            _userAuthenticationWriteRepository = userAuthenticationWriteRepository;
            _userAuthenticationReadRepository = userAuthenticationReadRepository;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<UserAuthentication> LoginAuthentication(string code, string phoneNumber, string token)
        {
            var now = DateTime.UtcNow.AddHours(4).TimeOfDay;
            var authentication = await _userAuthenticationReadRepository.GetAsync(x => x.IsDisabled == false && x.Code == code && x.Token == token);
            var existAuthentication = await _userAuthenticationReadRepository.GetAsync(x => x.IsDisabled == false && x.Token == token);
            if (existAuthentication == null)
                throw new ExpirationDateException("Kodun müddəti bitmişdir! Təkrar giriş edin");
            if (existAuthentication.ExpirationDate.TimeOfDay < now)
            {
                existAuthentication.IsDisabled = true;
                await _userAuthenticationWriteRepository.SaveAsync();
                throw new ExpirationDateException("Kodun müddəti bitmişdir! Təkrar giriş edin");
            }

            if (authentication == null)
            {
                if (existAuthentication != null)
                {
                    if (existAuthentication.Count > 1)
                        existAuthentication.Count -= 1;
                    else
                    {
                        existAuthentication.IsDisabled = true;
                    }
                    await _userAuthenticationWriteRepository.SaveAsync();

                }
                throw new AuthenticationCodeException("Kod yanlışdır!");
            }
            return authentication;
        }

        public async Task UserCreate(string phoneNumber, string code)
        {
            AppUser newUser = new AppUser
            {
                UserName = phoneNumber,
                PhoneNumber = phoneNumber,
                IsAdmin = false,
                Balance = 0,
            };
            var result = await _userManager.CreateAsync(newUser, code);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    throw new Exception(error.Description);
                }
            }
            await _userManager.AddToRoleAsync(newUser, "User");
            await _userAuthenticationWriteRepository.SaveAsync();

        }

        public async Task UserLogin(string phoneNumber, string code, UserAuthentication authentication)
        {
            var now = DateTime.UtcNow.AddHours(4).TimeOfDay;
            if (authentication.ExpirationDate.TimeOfDay < now)
                throw new ExpirationDateException("Kodun müddəti bitmişdir! Təkrar giriş edin");

            var UserExists = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == phoneNumber && x.IsAdmin == false);
            if (UserExists == null)
                throw new NotFoundException("NotFound");

            var result = await _signInManager.PasswordSignInAsync(UserExists, code, false, false);
            if (!result.Succeeded)
                throw new NotFoundException("NotFound");

            authentication.IsDisabled = true;
            await _userAuthenticationWriteRepository.SaveAsync();
        }

        public async Task UserResetPassword(string phoneNumber, string code)
        {
            var UserExists = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
            var tokenResetPassword = await _userManager.GeneratePasswordResetTokenAsync(UserExists);

            if (UserExists == null || !await _userManager.VerifyUserTokenAsync(UserExists, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", tokenResetPassword))
            {
                throw new NotFoundException("NotFound");
            }

            var result = await _userManager.ResetPasswordAsync(UserExists, tokenResetPassword, code);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    throw new Exception(error.Description);
                }
            }

        }
    }
}
