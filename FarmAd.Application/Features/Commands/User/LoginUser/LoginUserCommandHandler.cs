using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Tokens;
using FarmAd.Application.DTOs;
using FarmAd.Application.Exceptions;
using FarmAd.Domain.Entities.Identity;
using FarmAd.Infrastructure.Service.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace FarmAd.Application.Features.Commands.User.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly ILogger<LoginUserCommandHandler> _logger;

        public LoginUserCommandHandler(SignInManager<AppUser> signInManager, IUserService userService, UserManager<AppUser> userManager, ITokenHandler tokenHandler, ILogger<LoginUserCommandHandler> logger)
        {
            _signInManager = signInManager;
            _userService = userService;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _logger = logger;
        }
        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByNameAsync(request.UserNameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
            if (user == null)
                throw new NotFoundUserException("Istifadeci tapilmadi");

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccesToken(400, user);

                //await _userService.UpdateRefreshToken(token.RefreshToken, user.Id, token.Expiration, 405);
                _logger.LogInformation(user.Name + "-hesaba daxil oldu.");
                return new LoginUserCommandResponse()
                {
                    Token = token,
                    Message = "Daxil oldunuz " + user.Name + "bəy"

                };

            }
            return new()
            {
                Succes = false,
                Message = "Tapılmadı istifadeçi"
            };
        }
    }
}
