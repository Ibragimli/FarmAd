using FarmAd.Application.Exceptions;
using FarmAd.Domain.Entities.Identity;
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

namespace FarmAd.Infrastructure.Service.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<List<AppUser>> GetAllUsersAsync(int page, int size)
        {
            List<AppUser> users = await _userManager.Users.Skip(page * size).Take(size).ToListAsync();
            return users;
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
    }
}
