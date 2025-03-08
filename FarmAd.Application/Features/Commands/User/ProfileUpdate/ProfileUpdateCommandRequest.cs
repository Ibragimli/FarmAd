using FarmAd.Application.DTOs.User;
using MediatR;

namespace FarmAd.Application.Features.Commands.User.ProfileUpdate
{
    public class ProfileUpdateCommandRequest : IRequest<ProfileUpdateCommandResponse>
    {
        public ProfileEditDto ProfileEditDto { get; set; }
    }
}
