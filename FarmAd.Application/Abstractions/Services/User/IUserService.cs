using FarmAd.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Infrastructure.Service.User
{
    public interface IUserService
    {
        Task<List<AppUser>> GetAllUsersAsync(int page, int size);

        Task<AppUser> GetAsync(Expression<Func<AppUser, bool>> predicate);
        Task<AppUser> GetUserAsync(string UsernameOrEmail);
        Task<AppUser> GetUserIdAsync(string id);
        Task<bool> IsExistAsync(string id);
        Task SignOutUser();
        Task AssingRoleToUserAsync(string userId, string[] roles);
        int TotalUserCount { get; }

        Task<string[]> GetRolesToUserAsync(string userIdOrUsername);
        Task<bool> HasRolePermissionToEndpointAsync(string username, string code);


    }
}
