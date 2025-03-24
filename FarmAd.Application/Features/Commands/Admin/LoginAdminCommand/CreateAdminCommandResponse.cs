using FarmAd.Application.DTOs;

namespace FarmAd.Application.Features.Commands.Admin.LoginAdminCommand
{
    public class LoginAdminCommandResponse
    {
        public bool Succed { get; set; }
        public Token Token { get; set; }
    }
}
