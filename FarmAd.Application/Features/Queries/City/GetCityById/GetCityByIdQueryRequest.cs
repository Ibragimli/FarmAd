using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Queries.City.GetCityById
{
    public class GetCityByIdQueryRequest:IRequest<GetCityByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
