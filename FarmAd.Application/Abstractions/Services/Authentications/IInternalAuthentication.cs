using FarmAd.Application.DTOs;

namespace FarmAd.Application.Abstractions.Services.Authentications
{
    public interface IInternalAuthentication
    {
        Task<Token> RefreshTokenLoginAsync(string refreshToken);
    }
}
