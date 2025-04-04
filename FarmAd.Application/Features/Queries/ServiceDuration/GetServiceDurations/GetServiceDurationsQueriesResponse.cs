using FarmAd.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Queries.ServiceDuration.GetServiceDurations
{
    public class GetServiceDurationsQueriesResponse
    {
        public IQueryable<object> Datas { get; set; }
    }
}
