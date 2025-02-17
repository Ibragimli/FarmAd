using FarmAd.Application.DTOs;
using FarmAd.Application.DTOs.User;

namespace FarmAd.Application.Features.Commands.User.LoginAuthentication
{
    public class LoginAuthenticationCommandResponse
    {
        public Token Token { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public bool Succes { get; set; }
    }
}
