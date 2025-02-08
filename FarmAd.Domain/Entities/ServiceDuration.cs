using FarmAd.Domain.Entities.Common;
using FarmAd.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Domain.Entities
{
    public class ServiceDuration : BaseEntity
    {
        public decimal? Amount { get; set; }
        public int Duration { get; set; }
        public ServiceType ServiceType { get; set; }
    }
}
