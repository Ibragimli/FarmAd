using FarmAd.Application.DTOs.Area;
using MediatR;

namespace FarmAd.Application.Features.Commands.Admin.LoginAdminCommand
{
    public class LoginAdminCommandRequest : IRequest<LoginAdminCommandResponse>
    {
        public AdminLoginPostDto AdminLoginPostDto { get; set; }
    }
}
