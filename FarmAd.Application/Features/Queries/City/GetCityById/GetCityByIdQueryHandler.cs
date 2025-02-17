using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.Area;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Queries.City.GetCityById
{
    public class GetCityByIdQueryHandler : IRequestHandler<GetCityByIdQueryRequest, GetCityByIdQueryResponse>
    {
        private readonly IAdminCityServices _CityService;

        public GetCityByIdQueryHandler(IAdminCityServices CityService)
        {
            _CityService = CityService;
        }
        public async Task<GetCityByIdQueryResponse> Handle(GetCityByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _CityService.GetCity(request.Id);
            return new()
            {
                Id = data.Id,
                Name = data.Name
            };
        }
    }
}
