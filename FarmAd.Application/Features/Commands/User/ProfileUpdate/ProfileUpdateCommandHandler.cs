using FarmAd.Application.Abstractions.Services.User;
using FarmAd.Application.Exceptions;
using FarmAd.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FarmAd.Application.Features.Commands.User.ProfileUpdate
{
    public class ProfileUpdateCommandHandler : IRequestHandler<ProfileUpdateCommandRequest, ProfileUpdateCommandResponse>
    {
        private readonly IProfileEditServices _profileEditServices;

        public ProfileUpdateCommandHandler(IProfileEditServices profileEditServices)
        {
            _profileEditServices = profileEditServices;
        }

        public async Task<ProfileUpdateCommandResponse> Handle(ProfileUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            await _profileEditServices.Edit(request.ProfileEditDto);
            return new()
            {
                Message = "Succeeded"
            };
        }
    }
}
