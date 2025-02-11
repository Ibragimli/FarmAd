using FarmAd.Application.Features.Commands.User.LoginUser;
using MediatR;

namespace FarmAd.Application.Features.Commands.User.LoginAuthentication
{
    public class LoginAuthenticationCommandRequest : IRequest<LoginAuthenticationCommandResponse>
    {
        public string Username { get; set; }
        public string Code { get; set; }
        public string Token { get; set; }
    }
}
