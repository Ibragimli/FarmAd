using FarmAd.Application.Abstractions.Services.Area;
using FarmAd.Application.DTOs;
using FarmAd.Application.Exceptions;
using FarmAd.Application.Features.Commands.Admin.LoginAdminCommand;
using FarmAd.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FarmAd.Application.Features.Commands.Admin.CreateAdmin
{
    public class LoginAdminCommandHandler : IRequestHandler<LoginAdminCommandRequest, LoginAdminCommandResponse>
    {
        private readonly IAdminLoginServices _adminLoginServices;

        public LoginAdminCommandHandler(IAdminLoginServices adminLoginServices)
        {
            _adminLoginServices = adminLoginServices;
        }
        public async Task<LoginAdminCommandResponse> Handle(LoginAdminCommandRequest request, CancellationToken cancellationToken)
        {

            Token token = await _adminLoginServices.Login(request.AdminLoginPostDto);
            return new()
            {
                Succed = true,
                Token = token
            };
        }
    }
}
