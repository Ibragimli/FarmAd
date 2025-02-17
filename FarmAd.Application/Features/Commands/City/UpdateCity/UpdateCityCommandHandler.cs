using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.Area;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.City.UpdateCity
{
    public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommandRequest, UpdateCityCommandResponse>
    {
        private readonly IAdminCityServices _CityService;

        public UpdateCityCommandHandler(IAdminCityServices CityService)
        {
            _CityService = CityService;
        }
        public async Task<UpdateCityCommandResponse> Handle(UpdateCityCommandRequest request, CancellationToken cancellationToken)
        {
            await _CityService.CityEdit(request.CityUpdateDto);
            return new()
            {
                Succeeded = true
            };
        }
    }
}
