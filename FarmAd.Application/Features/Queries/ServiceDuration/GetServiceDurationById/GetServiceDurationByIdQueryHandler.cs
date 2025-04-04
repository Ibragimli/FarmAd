using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.Area;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Queries.ServiceDuration.GetServiceDurationById
{
    public class GetServiceDurationByIdQueryHandler : IRequestHandler<GetServiceDurationByIdQueryRequest, GetServiceDurationByIdQueryResponse>
    {
        private readonly IAdminServiceDurationServices _adminServiceDurationServices;

        public GetServiceDurationByIdQueryHandler(IAdminServiceDurationServices adminServiceDurationServices)
        {
            _adminServiceDurationServices = adminServiceDurationServices;
        }
        public async Task<GetServiceDurationByIdQueryResponse> Handle(GetServiceDurationByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _adminServiceDurationServices.GetServiceDuration(request.Id);
            return new()
            {
                Id = data.Id,
                Duration = data.Duration,
                Amount = data.Amount,
                ServiceType = data.ServiceType
            };
        }
    }
}
