using FarmAd.Domain.Entities;

using FarmAd.Application.DTOs.User;
using FarmAd.Application.Abstractions.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Application.Repositories.Category;
using FarmAd.Application.Repositories.City;
using FarmAd.Application.Repositories.SubCategory;

namespace Ferma.Service.Services.Implementations.User
{

    public class ProductCreateIndexServices : IProductCreateIndexServices
    {
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly ISubCategoryReadRepository _subCategoryReadRepository;
        private readonly ICityReadRepository _cityReadRepository;

        public ProductCreateIndexServices(ICategoryReadRepository categoryReadRepository, ISubCategoryReadRepository subCategoryReadRepository, ICityReadRepository cityReadRepository)
        {
            _categoryReadRepository = categoryReadRepository;
            _subCategoryReadRepository = subCategoryReadRepository;
            _cityReadRepository = cityReadRepository;
        }

        public async Task<List<Category>> Categories()
        {

            var list = await _categoryReadRepository.GetAllAsync(x => !x.IsDelete);

            return list.ToList();
        }

        public async Task<List<City>> Cities()
        {
            var list = await _cityReadRepository.GetAllAsync(x => !x.IsDelete);
            return list.ToList(); ;

        }

        public ProductCreateDto ProductDto()
        {
            ProductCreateDto ProductCreateDto = new ProductCreateDto();
            return ProductCreateDto;
        }

        public async Task<List<SubCategory>> SubCategories()
        {
            var list = await _subCategoryReadRepository.GetAllAsync(x => !x.IsDelete);
            return list.ToList(); ;
        }
    }
}
