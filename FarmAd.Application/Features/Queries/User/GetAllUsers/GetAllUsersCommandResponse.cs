using FarmAd.Application.DTOs;
using FarmAd.Application.DTOs.User;

namespace FarmAd.Application.Features.Queries.User.GetAllUsers
{
    public class GetAllUsersCommandResponse
    {
        public List<AllUserDto> AllUserDtos { get; set; }
    }
}
