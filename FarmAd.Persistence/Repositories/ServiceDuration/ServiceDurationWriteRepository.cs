using FarmAd.Application.Repositories.ServiceDuration;
using FarmAd.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Repositories.ServiceDuration
{
    public class ServiceDurationWriteRepository : WriteRepository<FarmAd.Domain.Entities.ServiceDuration>, IServiceDurationWriteRepository
    {
        public ServiceDurationWriteRepository(DataContext context) : base(context)
        {
        }
    }
}
