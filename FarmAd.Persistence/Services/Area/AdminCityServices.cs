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
        public async Task CityCreate(City City)
        {
            City newCity = new City();
            bool check = false;
            if (City.Name != null)
            {
                if (await _cityReadRepository.IsExistAsync(x => x.Name == City.Name))
                    throw new ItemAlreadyException("Bu adda şəhər mövcuddur!");

                if (City.Name.Length > 30)
                    throw new ItemFormatException("Şəhərin adı maksimum  uzunluğu 30 ola bilər");
                newCity.Name = City.Name;
                check = true;
            }
            else
                throw new ItemNullException("Şəhərin adı boş ola bilməz");

            if (check)
            {
                await _cityWriteRepository.AddAsync(newCity);
                await _cityWriteRepository.SaveAsync();
            }
        }

        public async Task CityEdit(City City)
        {
            bool check = false;
            var oldCity = await _cityReadRepository.GetAsync(x => x.Id == City.Id);

            if (City.Name != null)
            {
                if (await _cityReadRepository.IsExistAsync(x => x.Name == City.Name))
                    throw new ItemAlreadyException("Bu adda şəhər mövcuddur!");

                if (City.Name.Length > 30)
                    throw new ItemFormatException("Şəhərin adının maksimum  uzunluğu 30 ola bilər");
                if (City.Name != oldCity.Name)
                {
                    oldCity.Name = City.Name;
                    check = true;
                }
            }
            else
                throw new ItemNullException("Şəhər adı boş ola bilməz");

            if (check)
            {
                oldCity.ModifiedDate = DateTime.UtcNow.AddHours(4);
                await _cityWriteRepository.SaveAsync();
            }
        }

        public async Task CityDelete(int id)
        {
            var oldCity = await _cityReadRepository.GetAsync(x => x.Id == id);
            bool check = await _productFeatureReadRepository.IsExistAsync(x => x.CityId == id);
            if (check)
                throw new ItemUseException("Şəhər elanda istifadə olunur!!!");

            _cityWriteRepository.Remove(oldCity);

            await _cityWriteRepository.SaveAsync();

        }



        public async Task<City> GetCity(int id)
        {
            var City = await _cityReadRepository.GetAsync(x => x.Id == id && !x.IsDelete);

            return City;
        }
        public IQueryable<City> GetCities(string name)
        {
            var City = _cityReadRepository.AsQueryable();
            City = City.Where(x => !x.IsDelete);
            if (name != null)
                City = City.Where(i => EF.Functions.Like(i.Name, $"%{name}%"));

            return City;
        }
    }
}
