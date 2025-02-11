using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Tokens;
using FarmAd.Application.DTOs;
using FarmAd.Application.DTOs.User;
using FarmAd.Application.Exceptions;
using FarmAd.Domain.Entities.Identity;
using FarmAd.Infrastructure.Service.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace FarmAd.Application.Features.Commands.User.LoginAuthentication
{
    public class LoginAuthenticationCommandHandler : IRequestHandler<LoginAuthenticationCommandRequest, LoginAuthenticationCommandResponse>
    {
        private readonly IAuthService _authService;

        public LoginAuthenticationCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<LoginAuthenticationCommandResponse> Handle(LoginAuthenticationCommandRequest request, CancellationToken cancellationToken)
        {
           var token = await _authService.LoginAuthentication(request.Username, request.Code, request.Token);
            return new()
            {
                Succes = true,
                Token = token,
                Message = "true",
            };
        }
    }
}
