using FarmAd.Domain.Entities;

using FarmAd.Application.Exceptions;
using FarmAd.Application.Abstractions.Services.Area;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Application.Repositories.City;
using FarmAd.Application.Repositories.ProductFeature;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Identity;
using FarmAd.Application.DTOs.Area;

namespace FarmAd.Persistence.Service.Area
{
    public class AdminCityServices : IAdminCityServices
    {
        private readonly ICityReadRepository _cityReadRepository;
        private readonly ICityWriteRepository _cityWriteRepository;
        private readonly IProductFeatureReadRepository _productFeatureReadRepository;

        public AdminCityServices(ICityReadRepository cityReadRepository, ICityWriteRepository cityWriteRepository, IProductFeatureReadRepository productFeatureReadRepository)
        {
            _cityReadRepository = cityReadRepository;
            _cityWriteRepository = cityWriteRepository;
            _productFeatureReadRepository = productFeatureReadRepository;
        }
        public async Task CityCreate(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ItemNullException("Şəhərin adı boş ola bilməz");
            if (await _cityReadRepository.IsExistAsync(x => x.Name == name))
                throw new ItemAlreadyException("Bu adda şəhər mövcuddur!");

            if (name.Length > 30)
                throw new ItemFormatException("Şəhərin adı maksimum  uzunluğu 30 ola bilər");


            City newCity = new City
            {
                Name = name
            };
            await _cityWriteRepository.AddAsync(newCity);
            await _cityWriteRepository.SaveAsync();
        }

        public async Task CityEdit(CityUpdateDto cityUpdateDto)
        {

            if (string.IsNullOrEmpty(cityUpdateDto.Name))
                throw new ItemNullException("Şəhərin adı boş ola bilməz");
            if (await _cityReadRepository.IsExistAsync(x => x.Name == cityUpdateDto.Name))
                throw new ItemAlreadyException("Bu adda şəhər mövcuddur!");

            var oldCity = await _cityReadRepository.GetAsync(x => x.Id == cityUpdateDto.Id);

            if (oldCity == null)
                throw new ItemNotFoundException("Şəhər tapılmadı!");

            if (cityUpdateDto.Name.Length > 30)
                throw new ItemFormatException("Şəhərin adının maksimum  uzunluğu 30 ola bilər");

            if (cityUpdateDto.Name != oldCity.Name)
                oldCity.Name = cityUpdateDto.Name;

            oldCity.ModifiedDate = DateTime.UtcNow.AddHours(4);
            await _cityWriteRepository.SaveAsync();

        }

        public async Task CityDelete(int id)
        {
            var oldCity = await _cityReadRepository.GetAsync(x => x.Id == id);
            if (oldCity == null)
                throw new ItemNotFoundException("Şəhər tapılmadı!");

            bool isUsedInProduct = await _productFeatureReadRepository.IsExistAsync(x => x.CityId == id);
            if (isUsedInProduct)
                throw new ItemUseException("Şəhər elanda istifadə olunur!!!");

            _cityWriteRepository.Remove(oldCity);
            await _cityWriteRepository.SaveAsync();
        }



        public async Task<City> GetCity(int id)
        {
            var city = await _cityReadRepository.GetAsync(x => x.Id == id && !x.IsDelete);
            if (city == null)
                throw new ItemNotFoundException("Şəhər tapılmadı!");
            return city;
        }
        public (object, int) GetCities(string name, int page, int size)
        {
            var query = _cityReadRepository.GetAll();

            var cities = _cityReadRepository.GetAllPagenated(page, size).Where(x => !x.IsDelete);
            if (!string.IsNullOrEmpty(name))
                cities = cities.Where(i => EF.Functions.Like(i.Name, $"%{name}%"));
            return (cities.Select(r => new { r.Id, r.Name }), query.Count());

        }
    }
}
