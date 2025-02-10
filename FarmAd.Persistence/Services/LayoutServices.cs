using FarmAd.Domain.Entities;
using FarmAd.Application.Abstractions.Services.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Persistence.Contexts;
using FarmAd.Application.Repositories.Setting;
using FarmAd.Application.Repositories.City;

namespace FarmAd.Persistence.Services
{
    public class LayoutServices : ILayoutServices
    {
        private readonly ISettingReadRepository _settingReadRepository;
        private readonly ICityReadRepository _cityReadRepository;

        public LayoutServices(ISettingReadRepository settingReadRepository, ICityReadRepository cityReadRepository)
        {
            _settingReadRepository = settingReadRepository;
            _cityReadRepository = cityReadRepository;
        }
        public async Task<IEnumerable<Setting>> GetSettingsAsync()
        {
            var settings = await _settingReadRepository.GetAllAsync(x => !x.IsDelete);
            return settings;
        }
        public async Task<IEnumerable<City>> GetAllCitiesSearch()
        {
            var cities = await _cityReadRepository.GetAllAsync(x => !x.IsDelete);

            return cities;
        }

    }
}
