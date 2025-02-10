using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.Configurations;
using FarmAd.Application.Repositories.Endpoint;
using FarmAd.Application.Repositories.Menu;
using FarmAd.Domain.Entities;
using FarmAd.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Services.Configurations
{
    public class AuthorizationEndpointService : IAuthorizationEndpointService
    {
        private readonly IApplicationService _applicationService;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMenuReadRepository _menuReadRepository;
        private readonly IMenuWriteRepository _menuWriteRepository;
        private readonly IEndpointReadRepository _endpointReadRepository;
        private readonly IEndpointWriteRepository _endpointWriteRepository;

        public AuthorizationEndpointService(IApplicationService applicationService, RoleManager<AppRole> roleManager, IMenuReadRepository menuReadRepository, IMenuWriteRepository menuWriteRepository, IEndpointReadRepository endpointReadRepository, IEndpointWriteRepository endpointWriteRepository)
        {
            _applicationService = applicationService;
            _roleManager = roleManager;
            _menuReadRepository = menuReadRepository;
            _menuWriteRepository = menuWriteRepository;
            _endpointReadRepository = endpointReadRepository;
            _endpointWriteRepository = endpointWriteRepository;
        }

        public async Task AssignRoleEndpointAsync(string[] roles, string menu, string code, Type type)
        {
            Menu _menu = await _menuReadRepository.GetSingleAsync(m => m.Name == menu);
            if (_menu == null)
            {
                _menu = new()
                {
                    Name = menu
                };
                await _menuWriteRepository.AddAsync(_menu);
                await _endpointWriteRepository.SaveAsync();
            }

            Endpoint endpoint = await _endpointReadRepository.Table.Include(e => e.Menu).Include(e => e.Roles).FirstAsync(e => e.Code == code && e.Menu.Name == menu);
            if (endpoint == null)
            {
                var action = _applicationService.GetAuthorizeDefinitionEndpoint(type).FirstOrDefault(e => e.Name == menu)?.Actions.FirstOrDefault(e => e.Code == code);

                endpoint = new()
                {
                    Code = action.Code,
                    ActionType = action.ActionType,
                    HttpType = action.HttpType,
                    Definition = action.Definition,
                    Menu = _menu

                };
                await _endpointWriteRepository.AddAsync(endpoint);
                await _endpointWriteRepository.SaveAsync();
            }
            foreach (var role in endpoint.Roles)
            {
                endpoint.Roles.Remove(role);
            }
            var approles = await _roleManager.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();
            foreach (var role in approles)
                endpoint.Roles.Add(role);
            await _endpointWriteRepository.SaveAsync();

        }

        public async Task<List<string>> GetRolesToEndpoint(string code, string menu)
        {
            Endpoint? endpoint = await _endpointReadRepository.Table.Include(e => e.Roles).Include(e => e.Menu).FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);
            return endpoint.Roles.Select(r => r.Name).ToList();
        }
    }
}
