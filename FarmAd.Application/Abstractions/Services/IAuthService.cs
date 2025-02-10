using FarmAd.Application.Abstractions.Services.Authentications;

namespace FarmAd.Application.Abstractions.Services
{
    public interface IAuthService : IInternalAuthentication, IExternalAuthentication
    {
        Task PasswordResetAsync(string username);
        Task<bool> VerifyResetTokenAsync(string resetToken, string userId);
    }
}
