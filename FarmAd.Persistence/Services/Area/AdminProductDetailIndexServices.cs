using FarmAd.Domain.Entities;

using FarmAd.Application.Exceptions;
using FarmAd.Application.Abstractions.Services.Area;
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

namespace FarmAd.Persistence.Service.Area
{
    public class AdminProductDetailIndexServices : IAdminProductDetailIndexServices
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly ICityReadRepository _cityReadRepository;
        private readonly IProductUserIdReadRepository _productUserIdReadRepository;
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly ISubCategoryReadRepository _subCategoryReadRepository;

        public AdminProductDetailIndexServices(
            IProductReadRepository productReadRepository,
            ICityReadRepository cityReadRepository,
            IProductUserIdReadRepository productUserIdReadRepository,
            ICategoryReadRepository categoryReadRepository,
            ISubCategoryReadRepository subCategoryReadRepository)
        {
            _productReadRepository = productReadRepository;
            _cityReadRepository = cityReadRepository;
            _productUserIdReadRepository = productUserIdReadRepository;
            _categoryReadRepository = categoryReadRepository;
            _subCategoryReadRepository = subCategoryReadRepository;
        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await _productReadRepository.GetAsync(
                x => x.Id == id,
                "ProductFeatures.SubCategory",
                "ProductFeatures.SubCategory.Category",
                "ProductFeatures.City",
                "ProductImages");

            return product ?? throw new NotFoundException($"Product with ID {id} not found");
        }

        public async Task<List<SubCategory>> GetSubCategories()
        {
            return (await _subCategoryReadRepository.GetAllAsync(x => !x.IsDelete)).ToList();
        }

        public async Task<List<Category>> GetCategories()
        {
            return (await _categoryReadRepository.GetAllAsync(x => !x.IsDelete)).ToList();
        }

        public async Task<ProductUserId> GetAppUser(int productId)
        {
            var user = await _productUserIdReadRepository.GetAsync(
                x => !x.IsDelete && x.ProductId == productId, "AppUser");

            return user ?? throw new NotFoundException($"User with Product ID {productId} not found");
        }

        public async Task<List<City>> GetAllCities()
        {
            return (await _cityReadRepository.GetAllAsync(x => !x.IsDelete)).ToList();
        }
      
    }

}
