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

namespace FarmAd.Application.Features.Queries.ServiceDuration.GetServiceDurations
{
    public class GetServiceDurationsQueriesHandler : IRequestHandler<GetServiceDurationsQueriesRequest, GetServiceDurationsQueriesResponse>
    {
        private readonly IAdminServiceDurationServices _adminServiceDurationServices;

        public GetServiceDurationsQueriesHandler(IAdminServiceDurationServices adminServiceDurationServices)
        {
            _adminServiceDurationServices = adminServiceDurationServices;
        }
        public async Task<GetServiceDurationsQueriesResponse> Handle(GetServiceDurationsQueriesRequest request, CancellationToken cancellationToken)
        {
            var serviceDurations = _adminServiceDurationServices.GetServiceDurations();
            return new()
            {
                Datas = serviceDurations,
            };

        }
    }
}
