using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Tokens;
using FarmAd.Application.DTOs;
using FarmAd.Application.Exceptions;
using FarmAd.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ITokenHandler _tokenHandler;

        public RoleService(RoleManager<AppRole> roleManager, ITokenHandler tokenHandler)
        {
            _roleManager = roleManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<bool> CreateRole(string name)
        {
            bool nameExists = await _roleManager.Roles.AnyAsync(r => r.Name == name);
            if (nameExists)
                throw new RoleException("Bu adla rol artıq mövcuddur!");

            IdentityResult result = await _roleManager.CreateAsync(new() { Id = Guid.NewGuid().ToString(), Name = name });
            return result.Succeeded;
        }

        public async Task<bool> DeleteRole(string Id)
        {
            AppRole appRole = await _roleManager.FindByIdAsync(Id);
            IdentityResult result = await _roleManager.DeleteAsync(appRole);
            return result.Succeeded;
        }

        public (object, int) GetAllRoles(int page, int size)
        {
            var query = _roleManager.Roles;

            IQueryable<AppRole> rolesQuery = null;

            if (page != -1 && size != -1)
                rolesQuery = query.Skip(page * size).Take(size);
            else
                rolesQuery = query;

            return (rolesQuery.Select(r => new { r.Id, r.Name }), query.Count());
        }


        public async Task<(string id, string name)> GetRoleById(string id)
        {
            var role = await _roleManager.GetRoleIdAsync(new() { Id = id });
            return (id, role);
        }

        public async Task<bool> UpdateRole(string id, string name)
        {
            bool nameExists = await _roleManager.Roles.AnyAsync(r => r.Name == name);
            if (nameExists)
                throw new RoleException("Bu adla rol artıq mövcuddur!");

            AppRole? role = await _roleManager.FindByIdAsync(id);

            if (role == null)
                throw new RoleException();

            role.Name = name;
            IdentityResult result = await _roleManager.UpdateAsync(role);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    throw new RoleException(error.Description);
            }

            return result.Succeeded;
        }

    }
}
