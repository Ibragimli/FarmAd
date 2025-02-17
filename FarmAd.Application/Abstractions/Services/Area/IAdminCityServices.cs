using FarmAd.Application.DTOs.Area;
using FarmAd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.Area
{
    public interface IAdminCityServices
    {
        public (object, int) GetCities(string name, int page, int size);
        public Task<City> GetCity(int id);
        public Task CityCreate(string name);
        public Task CityEdit(CityUpdateDto cityUpdateDto);
        public Task CityDelete(int id);
    }
}
