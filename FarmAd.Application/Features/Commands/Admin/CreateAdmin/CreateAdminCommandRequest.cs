using MediatR;

namespace FarmAd.Application.Features.Commands.Admin.CreateAdmin
{
    public class CreateAdminCommandRequest : IRequest<CreateAdminCommandResponse>
    {
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
