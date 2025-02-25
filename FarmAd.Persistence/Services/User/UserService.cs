using FarmAd.Application.Exceptions;
using FarmAd.Application.Repositories.Endpoint;
using FarmAd.Application.Repositories.UserAuthentication;
using FarmAd.Domain.Entities;
using FarmAd.Domain.Entities.Identity;
using FarmAd.Infrastructure.Service.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserAuthenticationWriteRepository _userAuthenticationWriteRepository;
        private readonly IEndpointReadRepository _endpointReadRepository;
        private readonly SignInManager<AppUser> _signInManager;

        public UserService(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, IUserAuthenticationWriteRepository userAuthenticationWriteRepository, IEndpointReadRepository endpointReadRepository, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _userAuthenticationWriteRepository = userAuthenticationWriteRepository;
            _endpointReadRepository = endpointReadRepository;
            _signInManager = signInManager;
        }
        public async Task<List<AppUser>> GetAllUsersAsync(int page, int size)
        {
            List<AppUser> users = await _userManager.Users.Skip(page * size).Take(size).ToListAsync();
            return users;
        }
        public async Task<AppUser> CreateNewUser(string phoneNumber, string email, string fullname)
        {
            AppUser newUser = new AppUser();
            //hesab yaradmaq

            var UserExists = await GetAsync(x => x.PhoneNumber == phoneNumber);
            if (UserExists == null)
            {
                newUser = new AppUser
                {
                    UserName = phoneNumber,
                    PhoneNumber = phoneNumber,
                    IsAdmin = false,
                    Balance = 0,
                    Email = email,
                    Fullname = fullname,
                };
                var result = await _userManager.CreateAsync(newUser);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        throw new Exception(error.Description);
                    }
                }
                await _userManager.AddToRoleAsync(newUser, "User");
                await _userAuthenticationWriteRepository.SaveAsync();
                return newUser;
            }
            return UserExists;
            //hesab yaradmaq
        }
        public async Task<AppUser> GetAsync(Expression<Func<AppUser, bool>> predicate)

        {
            AppUser user = await _userManager.Users.FirstOrDefaultAsync(predicate);

            return user;
        }

        public async Task<AppUser> GetUserAsync(string UsernameOrEmail)
        {
            if (UsernameOrEmail == null)
                throw new ItemNullException("Paremetr boş ola bilməz");
            AppUser user = await _userManager.FindByNameAsync(UsernameOrEmail) ?? null;
            return user;
        }

        public async Task<AppUser> GetUserIdAsync(string id)
        {
            if (id == null)
                throw new ItemNullException("Paremetr boş ola bilməz");
            AppUser user = await _userManager.FindByIdAsync(id) ?? null;
            return user;
        }

        public async Task<bool> IsExistAsync(string id)
        {
            if (id == null)
                throw new ItemNullException("Paremetr boş ola bilməz");
            bool isExist = await _userManager.Users.AnyAsync(x => x.Id == id);
            return isExist;
        }
        public int TotalUserCount => _userManager.Users.Count();

        public async Task AssingRoleToUserAsync(string userId, string[] roles)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);
                await _userManager.AddToRolesAsync(user, roles);
            }

        }
        public async Task<string[]> GetRolesToUserAsync(string userIdOrUsername)
        {
            AppUser user = await _userManager.FindByIdAsync(userIdOrUsername);
            if (user == null)
                user = await _userManager.FindByNameAsync(userIdOrUsername);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                return userRoles.ToArray();
            }
            return new string[] { };
        }

        public async Task<bool> HasRolePermissionToEndpointAsync(string username, string code)
        {
            var userRoles = await GetRolesToUserAsync(username);
            if (!userRoles.Any())
                return false;
            FarmAd.Domain.Entities.Endpoint? endpoint = await _endpointReadRepository.Table.Include(e => e.Roles).FirstOrDefaultAsync(e => e.Code == code);
            if (endpoint == null)
                return false;

            var endpointRoles = endpoint.Roles.Select(r => r.Name);
            foreach (var userRole in userRoles)
            {
                foreach (var endpointRole in endpointRoles)
                {
                    if (userRole == endpointRole)
                        return true;
                }
            }
            return false;
        }

        public async Task SignOutUser() { await _signInManager.SignOutAsync(); }

        public bool IsIdendity(string username)
        {
            var isUser = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
            if (isUser)
                return true;
            return false;
        }
        public string IdentityUser()
        {
            var username = _httpContextAccessor.HttpContext.User.Identity.Name;
            if (username != null)
                return username;
            return null;
        }
    }

}
