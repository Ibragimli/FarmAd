using FarmAd.Application.Exceptions;
using FarmAd.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FarmAd.Application.Features.Commands.User.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {

            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.Username,
                Email = request.Email,
                Fullname = request.NameSurname

            }, request.Password);
            if (result.Succeeded)
            {
                return new()
                {
                    Succed = result.Succeeded,
                    Message = "Succeeded"

                };
            }
            throw new UserNotFoundException("Noo add");
        }
    }
}
