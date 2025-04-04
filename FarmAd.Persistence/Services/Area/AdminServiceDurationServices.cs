using FarmAd.Domain.Entities;

using FarmAd.Application.Exceptions;
using FarmAd.Application.Abstractions.Services.Area;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Application.Repositories.ServiceDuration;
using FarmAd.Application.DTOs.Area;
using FarmAd.Domain.Enums;

namespace FarmAd.Persistence.Service.Area
{
    public class AdminServiceDurationServices : IAdminServiceDurationServices
    {
        private readonly IServiceDurationReadRepository _serviceDurationReadRepository;
        private readonly IServiceDurationWriteRepository _serviceDurationWriteRepository;

        public AdminServiceDurationServices(IServiceDurationReadRepository serviceDurationReadRepository, IServiceDurationWriteRepository serviceDurationWriteRepository)
        {
            _serviceDurationReadRepository = serviceDurationReadRepository;
            _serviceDurationWriteRepository = serviceDurationWriteRepository;
        }
        public async Task ServiceDurationCreate(ServiceDurationCreatePostDto serviceDuration)
        {
            ValidateServiceDuration(serviceDuration);

            var newServiceDuration = new ServiceDuration
            {
                Duration = serviceDuration.Duration,
                Amount = serviceDuration.Amount,
                ServiceType = (serviceDuration.ServiceType == 1 ? Domain.Enums.ServiceType.Vip : Domain.Enums.ServiceType.Premium)
            };

            await _serviceDurationWriteRepository.AddAsync(newServiceDuration);
            await _serviceDurationWriteRepository.SaveAsync();
        }
        public async Task ServiceDurationEdit(ServiceDurationEditPostDto serviceDuration)
        {

            var oldServiceDuration = await _serviceDurationReadRepository.GetAsync(x => x.Id == serviceDuration.Id)
                ?? throw new ItemNullException("Xidmət müddəti tapılmadı");
           
            ValidateServiceDuration(serviceDuration);

            if (serviceDuration.Duration != 0)
                oldServiceDuration.Duration = serviceDuration.Duration;
            if (serviceDuration.Amount != 0)
                oldServiceDuration.Amount = serviceDuration.Amount;
            oldServiceDuration.ServiceType = (serviceDuration.ServiceType == 1 ? Domain.Enums.ServiceType.Vip : Domain.Enums.ServiceType.Premium);
            oldServiceDuration.ModifiedDate = DateTime.UtcNow.AddHours(4);

            await _serviceDurationWriteRepository.SaveAsync();
        }
        public async Task<ServiceDuration> GetServiceDuration(int id)
        {
            return await _serviceDurationReadRepository.GetAsync(x => x.Id == id && !x.IsDelete)
                ?? throw new ItemNullException("Xidmət müddəti tapılmadı");
        }
        public IQueryable<ServiceDuration> GetServiceDurations()
        {
            return _serviceDurationReadRepository.AsQueryable().Where(x => !x.IsDelete);
        }
        private void ValidateServiceDuration(object serviceDuration)
        {
            dynamic dto = serviceDuration;

            if (dto.Duration < 0)
                throw new ItemNullException("Vaxt boş ola bilməz");

            if (dto.Amount == null || dto.Amount < 0)
                throw new ItemNullException("Pul hissəsi boş ola bilməz");

            if (dto.ServiceType < 1 || dto.ServiceType > 2)
                throw new ItemNullException("Servis hissəsi yanlışdır");
        }
    }
}
