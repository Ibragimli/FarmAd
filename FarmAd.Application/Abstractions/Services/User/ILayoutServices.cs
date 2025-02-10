using FarmAd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.User
{
    public interface ILayoutServices
    {
        public Task<IEnumerable<Setting>> GetSettingsAsync();
        public Task<IEnumerable<City>> GetAllCitiesSearch();

    }
}
