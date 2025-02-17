using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.Area;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.City.DeleteCity
{
    public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommandRequest, DeleteCityCommandResponse>
    {
        private readonly IAdminCityServices _CityService;

        public DeleteCityCommandHandler(IAdminCityServices CityService)
        {
            _CityService = CityService;
        }
        public async Task<DeleteCityCommandResponse> Handle(DeleteCityCommandRequest request, CancellationToken cancellationToken)
        {
            await _CityService.CityDelete(request.Id);
            return new()
            {
                Succeeded = true
            };
        }
    }
}
