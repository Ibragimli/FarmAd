using FarmAd.Application.DTOs.Area;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.City.UpdateCity
{
    public class UpdateCityCommandRequest : IRequest<UpdateCityCommandResponse>
    {
        public CityUpdateDto CityUpdateDto { get; set; }
    }
}
