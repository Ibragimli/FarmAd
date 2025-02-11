using MediatR;

namespace FarmAd.Application.Features.Commands.User.LoginUser
{
    public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
    {
        public string Username { get; set; }
    }
}
