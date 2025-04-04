using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.Area;
using FarmAd.Application.Features.Commands.ServiceDuration.CreateServiceDuration;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.ServiceDuration.CreateServiceDuration
{
    public class CreateServiceDurationCommandHandler : IRequestHandler<CreateServiceDurationCommandRequest, CreateServiceDurationCommandResponse>
    {
        private readonly IAdminServiceDurationServices _adminServiceDurationServices;

        public CreateServiceDurationCommandHandler(IAdminServiceDurationServices adminServiceDurationServices)
        {
            _adminServiceDurationServices = adminServiceDurationServices;
        }
        public async Task<CreateServiceDurationCommandResponse> Handle(CreateServiceDurationCommandRequest request, CancellationToken cancellationToken)
        {
            await _adminServiceDurationServices.ServiceDurationCreate(request.ServiceDurationCreatePostDto);
            return new()
            {
                Succeeded = true
            };
        }
    }
}
