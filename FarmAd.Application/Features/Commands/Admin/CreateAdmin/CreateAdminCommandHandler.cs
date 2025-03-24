using FarmAd.Application.Exceptions;
using FarmAd.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FarmAd.Application.Features.Commands.Admin.CreateAdmin
{
    public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommandRequest, CreateAdminCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public CreateAdminCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<CreateAdminCommandResponse> Handle(CreateAdminCommandRequest request, CancellationToken cancellationToken)
        {

            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.Username,
                Fullname = request.Fullname

            }, request.Password);
            if (result.Succeeded)
            {
                return new()
                {
                    Succed = result.Succeeded,
                };
            }
            throw new UserNotFoundException("Noo add");
        }
    }
}
