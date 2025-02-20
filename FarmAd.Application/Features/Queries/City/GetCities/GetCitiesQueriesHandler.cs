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

namespace FarmAd.Application.Features.Queries.City.GetCities
{
    public class GetCitiesQueriesHandler : IRequestHandler<GetCitiesQueriesRequest, GetCitiesQueriesResponse>
    {
        private readonly IAdminCityServices _adminCityServices;

        public GetCitiesQueriesHandler(IAdminCityServices adminCityServices)
        {
            _adminCityServices = adminCityServices;
        }
        public async Task<GetCitiesQueriesResponse> Handle(GetCitiesQueriesRequest request, CancellationToken cancellationToken)
        {
            var (datas, count) = _adminCityServices.GetCities(request.Name,request.Page, request.Size);
            return new()
            {
                Datas = datas,
                TotalCount = count
            };

        }
    }
}
