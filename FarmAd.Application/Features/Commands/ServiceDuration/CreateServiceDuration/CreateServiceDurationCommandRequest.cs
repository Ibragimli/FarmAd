using FarmAd.Application.DTOs.Area;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.ServiceDuration.CreateServiceDuration
{
    public class CreateServiceDurationCommandRequest : IRequest<CreateServiceDurationCommandResponse>
    {
        public ServiceDurationCreatePostDto ServiceDurationCreatePostDto { get; set; }
    }
}
