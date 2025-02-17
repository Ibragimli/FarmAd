using FarmAd.Application.Abstractions.Services.Authentications;
using FarmAd.Application.DTOs;
using FarmAd.Application.DTOs.User;
using FarmAd.Domain.Entities;
using FarmAd.Domain.Entities.Identity;

namespace FarmAd.Application.Abstractions.Services
{
    public interface IAuthService : IInternalAuthentication, IExternalAuthentication
    {
        Task<string> Login(string username);
        Task<Token> LoginAuthentication(string username, string code, string token);
        Task<bool> CreateUser(string username);
        Task<bool> CreateUserPostman(RegisterUserDto model);
        Task<List<AllUserDto>> GetAllUser();
        Task PasswordResetAsync(string username);
        Task<bool> VerifyResetTokenAsync(string resetToken, string userId);
        Task<bool> RareLimitAllDelete(bool isDisabled = true);
        Task<List<UserAuthentication>> GetAllUserAuthentication(int page, int size);
    }
}
