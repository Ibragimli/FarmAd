using FarmAd.Application.DTOs;

namespace FarmAd.Application.Features.Commands.User.LoginUser
{
    public class LoginUserCommandResponse
    {
        public string Message { get; set; }
        public Token Token { get; set; }
        public bool Succes { get; set; }
    }
}
