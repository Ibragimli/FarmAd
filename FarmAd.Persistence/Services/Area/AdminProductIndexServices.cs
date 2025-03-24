using FarmAd.Domain.Entities;
using FarmAd.Application.Exceptions;
using FarmAd.Application.DTOs.User;
using FarmAd.Application.Abstractions.Services.Area;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Domain.Enums;
using FarmAd.Application.Repositories.Product;
using FarmAd.Application.Repositories.SubCategory;
using FarmAd.Application.Repositories.Category;

namespace FarmAd.Persistence.Service.Area
{
    public class AdminProductIndexServices : IAdminProductIndexServices
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly ISubCategoryReadRepository _subCategoryReadRepository;
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public AdminProductIndexServices(
            IProductReadRepository productReadRepository,
            ISubCategoryReadRepository subCategoryReadRepository,
            ICategoryReadRepository categoryReadRepository,
            IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _subCategoryReadRepository = subCategoryReadRepository;
            _categoryReadRepository = categoryReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task DisableExpiredProducts()
        {
            var now = DateTime.UtcNow.AddHours(4);

            var products = (await _productReadRepository.GetAllAsync(
                x => !x.IsDelete && !x.ProductFeatures.IsDisabled && x.ProductFeatures.ExpirationDateDisabled < now,
                true, "ProductFeatures")).ToList();

            if (products.Any())
            {
                products.ForEach(product =>
                {
                    product.ProductFeatures.ProductStatus = ProductStatus.Disabled;
                    product.ProductFeatures.IsDisabled = true;
                });

                await _productWriteRepository.SaveAsync();
            }
        }

        public async Task<(IQueryable<Product>, int)> GetProducts(string name, string phoneNumber, int subCategoryId)
        {
            var products = _productReadRepository.asQueryableProduct().Where(x => !x.IsDelete);
            var count = await _productReadRepository.GetTotalCountAsync(x => !x.IsDelete);

            if (subCategoryId != 0)
                products = products.Where(x => x.ProductFeatures.SubCategoryId == subCategoryId);

            if (!string.IsNullOrWhiteSpace(name))
                products = products.Where(i => EF.Functions.Like(i.ProductFeatures.Name, $"%{name}%"));

            if (!string.IsNullOrWhiteSpace(phoneNumber))
                products = products.Where(i => EF.Functions.Like(i.ProductFeatures.PhoneNumber, $"%{phoneNumber}%"));

            return (products,count);
        }

        public async Task<List<SubCategory>> GetSubCategories()
        {
            var subCategories = await _subCategoryReadRepository.GetAllAsync(x => !x.IsDelete);
            return subCategories?.ToList() ?? throw new NotFoundException("Subcategories not found");
        }

        public async Task<List<Category>> GetCategories()
        {
            var categories = await _categoryReadRepository.GetAllAsync(x => !x.IsDelete);
            return categories?.ToList() ?? throw new NotFoundException("Categories not found");
        }

    }

}