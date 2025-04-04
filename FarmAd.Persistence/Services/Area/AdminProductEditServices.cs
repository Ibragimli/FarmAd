using FarmAd.Domain.Entities;
using FarmAd.Application.Exceptions;
using FarmAd.Application.Abstractions.Services.Area;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Domain.Enums;
using FarmAd.Application.Repositories.Product;
using FarmAd.Application.DTOs.Area;
using FarmAd.Application.Repositories.SubCategory;

namespace FarmAd.Persistence.Service.Area
{
    public class AdminProductEditServices : IAdminProductEditServices
    {
        private readonly ISubCategoryReadRepository _subCategoryReadRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public AdminProductEditServices(ISubCategoryReadRepository subCategoryReadRepository, IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _subCategoryReadRepository = subCategoryReadRepository;
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task CheckPostEdit(AdminProductEditPostDto product)
        {
            if (product.Id == 0)
                throw new NotFoundException("Product ID is missing");


            if (product.SubCategoryId != 0 && !await _subCategoryReadRepository.IsExistAsync(x => x.Id == product.SubCategoryId))
                throw new ItemNullException("Category not found!");

            if (product.Name.Length > 100)
                throw new ItemNullException("Product name cannot exceed 100 characters");

            if (product.Describe.Length > 3000)
                throw new ItemNullException("Description cannot exceed 3000 characters");
        }

        public async Task EditProduct(AdminProductEditPostDto product)
        {
            var oldProduct = await _productReadRepository.GetAsync(x => x.Id == product.Id, "ProductFeatures");
            if (oldProduct == null)
                throw new NotFoundException($"Product not found with ID {product.Id}");

            if (string.IsNullOrWhiteSpace(product.Name))
                oldProduct.ProductFeatures.Name = product.Name;
            if (string.IsNullOrWhiteSpace(product.Describe))
                oldProduct.ProductFeatures.Describe = product.Describe;
            if (product.SubCategoryId != 0)
                oldProduct.ProductFeatures.SubCategoryId = product.SubCategoryId;
            await _productWriteRepository.SaveAsync();
        }

        public async Task ProductDisabled(int id)
        {
            var product = await _productReadRepository.GetAsync(x => x.Id == id, "ProductFeatures");
            if (product == null)
                throw new ItemNotFoundException($"Product not found with ID:{id}");

            var defaultTime = new DateTime(0001, 01, 01, 8, 36, 44);

            product.ProductFeatures.ProductStatus = ProductStatus.Disabled;
            product.ProductFeatures.IsPremium = false;
            product.ProductFeatures.IsVip = false;
            product.ProductFeatures.ExpirationDatePremium = defaultTime;
            product.ProductFeatures.ExpirationDateVip = defaultTime;

            await _productWriteRepository.SaveAsync();
        }

        public async Task ProductActive(int id)
        {
            var product = await _productReadRepository.GetAsync(x => x.Id == id, "ProductFeatures");
            if (product == null)
                throw new ItemNotFoundException($"Product not found with ID:{id}");

            var now = DateTime.UtcNow;

            product.ProductFeatures.ProductStatus = ProductStatus.Active;
            product.ProductFeatures.ExpirationDateActive = now.AddDays(30);
            product.ProductFeatures.ExpirationDateDisabled = now.AddDays(90);
            product.ProductFeatures.IsDisabled = false;

            await _productWriteRepository.SaveAsync();
        }


    }
}
