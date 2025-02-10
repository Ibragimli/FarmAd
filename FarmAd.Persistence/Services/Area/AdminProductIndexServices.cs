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

        public AdminProductIndexServices(IProductReadRepository productReadRepository, ISubCategoryReadRepository subCategoryReadRepository, ICategoryReadRepository categoryReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _subCategoryReadRepository = subCategoryReadRepository;
            _categoryReadRepository = categoryReadRepository;
            _productWriteRepository = productWriteRepository;
        }
        public async Task IsDisabled()
        {
            var now = DateTime.UtcNow.AddHours(4);
            var ValidateProduct = await _productReadRepository.IsExistAsync(x => !x.IsDelete && !x.ProductFeatures.IsDisabled && x.ProductFeatures.ExpirationDateDisabled < now, true, "ProductFeatures");
            if (ValidateProduct)
            {
                var Products = await _productReadRepository.GetAllAsync(x => !x.IsDelete && !x.ProductFeatures.IsDisabled && x.ProductFeatures.ExpirationDateDisabled < now, true, "ProductFeatures");
                foreach (var Product in Products)
                {
                    Product.ProductFeatures.ProductStatus = ProductStatus.Disabled;
                    Product.ProductFeatures.IsDisabled = true;
                    await _productWriteRepository.SaveAsync();
                }
            }
        }
        public IQueryable<Product> GetProduct(string name, string phoneNumber, int subCategoryId)
        {
            var Product = _productReadRepository.asQueryableProduct();
            Product = Product.Where(x => !x.IsDelete);
            if (subCategoryId != 0)
                Product = Product.Where(x => x.ProductFeatures.SubCategoryId == subCategoryId);
            if (name != null)
                Product = Product.Where(i => EF.Functions.Like(i.ProductFeatures.Name, $"%{name}%"));
            if (phoneNumber != null)
                Product = Product.Where(i => EF.Functions.Like(i.ProductFeatures.PhoneNumber, $"%{phoneNumber}%"));

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
            var subCategories = await _categoryReadRepository.GetAllAsync(x => !x.IsDelete);
            if (subCategories == null)
                throw new NotFoundException("Error");
            return subCategories.ToList();
        }


    }
}