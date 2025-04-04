using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.Area;
using FarmAd.Application.Features.Commands.ServiceDuration.UpdateServiceDuration;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.ServiceDuration.CreateServiceDuration
{
    public class UpdateServiceDurationCommandHandler : IRequestHandler<UpdateServiceDurationCommandRequest, UpdateServiceDurationCommandResponse>
    {
        private readonly IAdminServiceDurationServices _adminServiceDurationServices;

        public UpdateServiceDurationCommandHandler(IAdminServiceDurationServices adminServiceDurationServices)
        {
            _adminServiceDurationServices = adminServiceDurationServices;
        }
        public async Task<UpdateServiceDurationCommandResponse> Handle(UpdateServiceDurationCommandRequest request, CancellationToken cancellationToken)
        {
            await _adminServiceDurationServices.ServiceDurationEdit(request.ServiceDurationEditPostDto);
            return new()
            {
                Succeeded = true
            };
        }
    }
}
