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

namespace FarmAd.Persistence.Service.Area
{
    public class AdminProductEditServices : IAdminProductEditServices
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public AdminProductEditServices(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public void CheckPostEdit(AdminProductEditPostDto product)
        {
            if (product.Id == 0)
                throw new NotFoundException("Product ID is missing");

            if (product.SubCategoryId == 0)
                throw new ItemNullException("Category cannot be empty");

            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ItemNullException("Product name cannot be empty");

            if (string.IsNullOrWhiteSpace(product.Describe))
                throw new ItemNullException("Description cannot be empty");

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

            oldProduct.ProductFeatures.Name = product.Name;
            oldProduct.ProductFeatures.Describe = product.Describe;
            oldProduct.ProductFeatures.SubCategoryId = product.SubCategoryId;

            await _productWriteRepository.SaveAsync();
        }

        public async Task ProductDisabled(int id)
        {
            var product = await _productReadRepository.GetAsync(x => x.Id == id);
            if (product == null)
                throw new ItemNotFoundException($"Product not found with ID {id}");

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
            var product = await _productReadRepository.GetAsync(x => x.Id == id);
            if (product == null)
                throw new ItemNotFoundException($"Product not found with ID {id}");

            var now = DateTime.UtcNow;

            product.ProductFeatures.ProductStatus = ProductStatus.Active;
            product.ProductFeatures.ExpirationDateActive = now.AddDays(30);
            product.ProductFeatures.ExpirationDateDisabled = now.AddDays(90);
            product.ProductFeatures.IsDisabled = false;

            await _productWriteRepository.SaveAsync();
        }
    }
}
