using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.Area;
using FarmAd.Application.Repositories.City;
using FarmAd.Domain.Entities.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Queries.Role.GetRoles
{
    public class GetCategoriesQueriesHandler : IRequestHandler<GetCategoriesQueriesRequest, GetCategoriesQueriesResponse>
    {
        private readonly IAdminCategoryServices _adminCategoryServices;

        public GetCategoriesQueriesHandler(IAdminCategoryServices adminCityServices)
        {
            _adminCategoryServices = adminCityServices;
        }
        public async Task<GetCategoriesQueriesResponse> Handle(GetCategoriesQueriesRequest request, CancellationToken cancellationToken)
        {
            var (datas, count) = _adminCategoryServices.GetCategories(request.Name, request.Page, request.Size);
            return new()
            {
                Datas = datas,
                TotalCount = count
            };

        }
    }

}
