using FarmAd.Application.Abstractions.Services;
using FarmAd.Domain.Entities.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Queries.Role.GetRoles
{
    public class GetRolesQueriesHandler : IRequestHandler<GetRolesQueriesRequest, GetRolesQueriesResponse>
    {
        private readonly IRoleService _roleService;

        public GetRolesQueriesHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public async Task<GetRolesQueriesResponse> Handle(GetRolesQueriesRequest request, CancellationToken cancellationToken)
        {
            var (datas, count) = _roleService.GetAllRoles(request.Page, request.Size);
            return new()
            {
                Datas = datas,
                TotalCount = count
            };

        }
    }
}
