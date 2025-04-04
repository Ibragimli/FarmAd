using FarmAd.Application.DTOs.Area;
using FarmAd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.Area
{
    public interface IAdminServiceDurationServices
    {
        public IQueryable<ServiceDuration> GetServiceDurations();
        public Task<ServiceDuration> GetServiceDuration(int id);
        public Task ServiceDurationCreate(ServiceDurationCreatePostDto ServiceDuration);
        public Task ServiceDurationEdit(ServiceDurationEditPostDto ServiceDuration);
    }
}
