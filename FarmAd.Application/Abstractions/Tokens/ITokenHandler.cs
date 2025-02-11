using FarmAd.Application.DTOs;
using FarmAd.Domain.Entities.Identity;

namespace FarmAd.Application.Abstractions.Tokens
{
    public interface ITokenHandler
    {
        Token CreateAccesToken(int minute,AppUser user);
        string CreateRefreshToken();
        bool ValidateToken(string token);



    }
}
