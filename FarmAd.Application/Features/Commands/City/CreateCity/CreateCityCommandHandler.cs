using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.Area;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.City.CreateCity
{
    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommandRequest, CreateCityCommandResponse>
    {
        private readonly IAdminCityServices _CityService;

        public CreateCityCommandHandler(IAdminCityServices CityService)
        {
            _CityService = CityService;
        }
        public async Task<CreateCityCommandResponse> Handle(CreateCityCommandRequest request, CancellationToken cancellationToken)
        {
            await _CityService.CityCreate(request.Name);
            return new()
            {
                Succeeded = true
            };
        }
    }
}
