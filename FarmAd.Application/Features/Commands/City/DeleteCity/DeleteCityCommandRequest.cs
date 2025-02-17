using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.City.DeleteCity
{
    public class DeleteCityCommandRequest : IRequest<DeleteCityCommandResponse>
    {
        public int Id { get; set; }
    }
}
