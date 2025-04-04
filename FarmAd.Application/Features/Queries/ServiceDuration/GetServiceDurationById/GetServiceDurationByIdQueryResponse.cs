using FarmAd.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Queries.ServiceDuration.GetServiceDurationById
{
    public class GetServiceDurationByIdQueryResponse
    {
        public int Id { get; set; }
        public ServiceType ServiceType { get; set; }
        public decimal? Amount { get; set; }
        public int Duration { get; set; }
    }
}
