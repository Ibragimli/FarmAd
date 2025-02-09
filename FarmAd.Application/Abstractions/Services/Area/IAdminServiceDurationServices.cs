using FarmAd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferma.Service.Services.Interfaces.Area
{
    public interface IAdminServiceDurationServices
    {
        public IQueryable<ServiceDuration> GetServiceDurations();
        public Task<ServiceDuration> GetServiceDuration(int id);
        public Task ServiceDurationCreate(ServiceDuration ServiceDuration);
        public Task ServiceDurationEdit(ServiceDuration ServiceDuration);
    }
}
