using FarmAd.Application.DTOs.Area;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.ServiceDuration.UpdateServiceDuration
{
    public class UpdateServiceDurationCommandRequest : IRequest<UpdateServiceDurationCommandResponse>
    {
        public ServiceDurationEditPostDto ServiceDurationEditPostDto { get; set; }
    }
}
