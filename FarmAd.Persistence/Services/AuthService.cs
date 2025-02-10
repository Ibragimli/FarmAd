using FarmAd.Application.Abstractions.Helpers;
using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Tokens;
using FarmAd.Application.DTOs;
using FarmAd.Domain.Entities.Identity;
using FarmAd.Infrastructure.Service.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace FarmAd.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(UserManager<AppUser> userManager,  ITokenHandler tokenHandler, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task PasswordResetAsync(string username)
        {
            AppUser user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                //resetToken = resetToken.UrlEncode();

                //await _mailService.SendPasswordResetMailAsync(user.Email, user.Id, resetToken);

            }

        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            //AppUser? user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);

            //if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            //{
            //    Token token = _tokenHandler.CreateAccesToken(500, user);
            //    await _userService.UpdateRefreshToken(token.RefreshToken, user.Id, token.Expiration, 500);
            //    return token;
            //}
            //else
                throw new Exception("user tapilmadi");
        }

        public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {

                //resetToken = resetToken.UrlDecode();
                return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);
            }
            return false;
        }
    }
}
