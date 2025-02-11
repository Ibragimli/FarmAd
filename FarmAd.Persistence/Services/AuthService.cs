using FarmAd.Application.Abstractions.Helpers;
using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.User;
using FarmAd.Application.Abstractions.Tokens;
using FarmAd.Application.DTOs;
using FarmAd.Application.DTOs.User;
using FarmAd.Application.Exceptions;
using FarmAd.Application.Repositories.UserAuthentication;
using FarmAd.Domain.Entities;
using FarmAd.Domain.Entities.Identity;
using FarmAd.Infrastructure.Service.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace FarmAd.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserAuthenticationReadRepository _userAuthenticationReadRepository;
        private readonly IOTPService _oTPService;
        private readonly IUserAuthenticationWriteRepository _userAuthenticationWriteRepository;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(UserManager<AppUser> userManager, IUserAuthenticationReadRepository userAuthenticationReadRepository, IOTPService OTPService, IUserAuthenticationWriteRepository userAuthenticationWriteRepository, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _userAuthenticationReadRepository = userAuthenticationReadRepository;
            _oTPService = OTPService;
            _userAuthenticationWriteRepository = userAuthenticationWriteRepository;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> CreateUserPostman(RegisterUserDto model)
        {
            var user = new AppUser
            {
                UserName = model.Username,
                Fullname = model.Fullname,
                IsAdmin = false,
                Balance = 0

            };
            var code = _oTPService.CodeCreate();
            var result = await _userManager.CreateAsync(user, code);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    throw new Exception(error.Description);
                }
            }
            await _userManager.AddToRoleAsync(user, "User");
            await _userAuthenticationWriteRepository.SaveAsync();
            return true;
        }

        public async Task<bool> CreateUser(string username)
        {
            var user = new AppUser
            {
                UserName = username,
                Fullname = null,
                IsAdmin = false,
                Balance = 0

            };
            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
                foreach (var error in result.Errors)
                    throw new Exception(error.Description);

            await _userManager.AddToRoleAsync(user, "User");
            await _userAuthenticationWriteRepository.SaveAsync();
            return true;
        }


        public async Task<List<AllUserDto>> GetAllUser()
        {
            var users = await _userManager.Users.ToListAsync();
            List<AllUserDto> dtos = new();
            foreach (var user in users)
            {
                AllUserDto allUserDto = new AllUserDto()
                {
                    Id = user.Id,
                    Username = user.UserName,
                    //Token = user.Token,
                    RefreshToken = user.RefreshToken,
                    RefreshTokenDate = user.RefreshTokenEndDate
                };
                dtos.Add(allUserDto);
            }
            return dtos;
        }

        public async Task<string> Login(string username)
        {
            bool userExist = await _userManager.Users.AnyAsync(x => x.UserName == username);
            if (!userExist)
                await CreateUser(username);

            var code = _oTPService.CodeCreate();
            var user = await _userManager.FindByNameAsync(username);
            var auth = await _oTPService.CreateAuthentication(code, username);
            Token token = _tokenHandler.CreateAccesToken(10, user);
            return token.AccesToken;
        }

        public async Task<Token> LoginAuthentication(string username, string code, string token)
        {
            if (!_tokenHandler.ValidateToken(token))
                throw new UnauthorizedUserException();

            var now = DateTime.UtcNow.AddHours(4).TimeOfDay;
            AppUser user = await _userManager.FindByNameAsync(username);
            if (user == null)
                throw new NotFoundUserException();

            var authentication = await _userAuthenticationReadRepository
                .GetAsync(x => x.IsDisabled == false && x.Username == username && x.Code == code);

            if (authentication == null)
                throw new AuthenticationCodeException("Kod yanlışdır!");

            if (authentication.ExpirationDate.TimeOfDay < now)
            {
                authentication.IsDisabled = true;
                await _userAuthenticationWriteRepository.SaveAsync();
                throw new ExpirationDateException();
            }
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(user, isPersistent: false);
            Token newToken = _tokenHandler.CreateAccesToken(10, user);

            return newToken;
        }

        //public async Task<UserAuthentication> LoginAuthentication(string code, string phoneNumber, string token)
        //{
        //    var now = DateTime.UtcNow.AddHours(4).TimeOfDay;
        //    var authentication = await _unitOfWork.UserAuthenticationRepository.GetAsync(x => x.IsDisabled == false && x.Code == code && x.Token == token);
        //    var existAuthentication = await _unitOfWork.UserAuthenticationRepository.GetAsync(x => x.IsDisabled == false && x.Token == token);
        //    if (existAuthentication == null)
        //        throw new ExpirationDateException("Kodun müddəti bitmişdir! Təkrar giriş edin");
        //    if (existAuthentication.ExpirationDate.TimeOfDay < now)
        //    {
        //        existAuthentication.IsDisabled = true;
        //        await _unitOfWork.CommitAsync();
        //        throw new ExpirationDateException("Kodun müddəti bitmişdir! Təkrar giriş edin");
        //    }

        //    if (authentication == null)
        //    {
        //        if (existAuthentication != null)
        //        {
        //            if (existAuthentication.Count > 1)
        //                existAuthentication.Count -= 1;
        //            else
        //            {
        //                existAuthentication.IsDisabled = true;
        //            }
        //            await _unitOfWork.CommitAsync();
        //        }
        //        throw new AuthenticationCodeException("Kod yanlışdır!");
        //    }
        //    return authentication;
        //}

        //public async Task UserCreate(string phoneNumber, string code)
        //{
        //    AppUser newUser = new AppUser
        //    {
        //        UserName = phoneNumber,
        //        PhoneNumber = phoneNumber,
        //        IsAdmin = false,
        //        Balance = 0,
        //    };
        //    var result = await _userManager.CreateAsync(newUser, code);
        //    if (!result.Succeeded)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            throw new Exception(error.Description);
        //        }
        //    }
        //    await _userManager.AddToRoleAsync(newUser, "User");
        //    await _unitOfWork.CommitAsync();
        //}

        //public async Task UserLogin(string phoneNumber, string code, UserAuthentication authentication)
        //{
        //    var now = DateTime.UtcNow.AddHours(4).TimeOfDay;
        //    if (authentication.ExpirationDate.TimeOfDay < now)
        //        throw new ExpirationDateException("Kodun müddəti bitmişdir! Təkrar giriş edin");

        //    var UserExists = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == phoneNumber && x.IsAdmin == false);
        //    if (UserExists == null)
        //        throw new NotFoundException("NotFound");

        //    var result = await _signInManager.PasswordSignInAsync(UserExists, code, false, false);
        //    if (!result.Succeeded)
        //        throw new NotFoundException("NotFound");

        //    authentication.IsDisabled = true;
        //    await _unitOfWork.CommitAsync();


        //}

        //public async Task UserResetPassword(string phoneNumber, string code)
        //{
        //    var UserExists = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
        //    var tokenResetPassword = await _userManager.GeneratePasswordResetTokenAsync(UserExists);

        //    if (UserExists == null || !(await _userManager.VerifyUserTokenAsync(UserExists, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", tokenResetPassword)))
        //    {
        //        throw new NotFoundException("NotFound");
        //    }

        //    var result = await _userManager.ResetPasswordAsync(UserExists, tokenResetPassword, code);

        //    if (!result.Succeeded)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            throw new Exception(error.Description);
        //        }
        //    }

        //}

        ///
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
