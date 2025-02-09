using FarmAd.Domain.Entities;

using FarmAd.Application.Exceptions;
using Ferma.Service.Services.Interfaces.Area;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using FarmAd.Application.Repositories.Product;
using FarmAd.Application.Repositories.SubCategory;
using FarmAd.Application.Repositories.Category;
using FarmAd.Application.Repositories.City;

namespace Ferma.Service.Services.Implementations.Area
{
    public class AdminProductDetailIndexServices : IAdminProductDetailIndexServices
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly ICityReadRepository _cityReadRepository;
        private readonly IProductUserIdReadRepository _productUserIdReadRepository;
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly ISubCategoryReadRepository _subCategoryReadRepository;

        public AdminProductDetailIndexServices(IProductReadRepository productReadRepository, ICityReadRepository cityReadRepository, IProductUserIdReadRepository productUserIdReadRepository, ICategoryReadRepository categoryReadRepository, ISubCategoryReadRepository subCategoryReadRepository)
        {
            _productReadRepository = productReadRepository;
            _cityReadRepository = cityReadRepository;
            _productUserIdReadRepository = productUserIdReadRepository;
            _categoryReadRepository = categoryReadRepository;
            _subCategoryReadRepository = subCategoryReadRepository;
        }
        public async Task<Product> GetProduct(int id)
        {
            var Product = await _productReadRepository.GetAsync(x => x.Id == id, "ProductFeatures.SubCategory", "ProductFeatures.SubCategory.Category", "ProductFeatures.City", "ProductImages");
            if (Product == null)
                throw new NotFoundException("Error");
            return Product;
        }

        public async Task<List<SubCategory>> GetSubCategories()
        {
            var subCategories = await _subCategoryReadRepository.GetAllAsync(x => !x.IsDelete);
            if (subCategories == null)
                throw new NotFoundException("Error");
            return subCategories.ToList();
        }
        public async Task<List<Category>> GetCategories()
        {
            var categories = await _categoryReadRepository.GetAllAsync(x => !x.IsDelete);
            if (categories == null)
                throw new NotFoundException("Error");
            return categories.ToList();
        }
        public async Task<ProductUserId> GetAppUser(int ProductId)
        {
            var user = await _productUserIdReadRepository.GetAsync(x => !x.IsDelete && x.ProductId == ProductId, "AppUser");
            if (user == null)
                throw new NotFoundException("Error");
            return user;
        }

        public async Task<List<City>> GetAllCity()
        {
            var cities = await _cityReadRepository.GetAllAsync(x => !x.IsDelete);
            if (cities == null)
                throw new NotFoundException("Error");
            return cities.ToList();
        }
    }
}
