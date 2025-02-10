using FarmAd.Domain.Entities;
using FarmAd.Domain.Enums;

using FarmAd.Application.DTOs.User;
using FarmAd.Application.Abstractions.Services.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarmAd.Application.Repositories.Product;

namespace FarmAd.Persistence.Services.User
{
    public class ProductSearchServices : IProductSearchServices
    {
        private readonly IProductReadRepository _productReadRepository;

        public ProductSearchServices(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }
        public IQueryable<Product> SearchProductAll(SearchDto searchDto)
        {
            var Product = _productReadRepository.asQueryableProduct();
            if (searchDto.CategoryId != null)
                Product = Product.Where(x => x.ProductFeatures.SubCategory.CategoryId == searchDto.CategoryId);

            if (searchDto.SubCategoryId != null)
                Product = Product.Where(x => x.ProductFeatures.SubCategoryId == searchDto.SubCategoryId);

            if (searchDto.CityId != null)
                Product = Product.Where(x => x.ProductFeatures.CityId == searchDto.CityId);

            if (searchDto.ProductName != null)
                Product = Product.Where(i => EF.Functions.Like(i.ProductFeatures.Name, $"%{searchDto.ProductName}%"));
            Product = Product.Where(x => !x.IsDelete && x.ProductFeatures.ProductStatus == ProductStatus.Active);

            return Product;
        }
        public IQueryable<Product> SearchProductVip(SearchDto searchDto)
        {
            var Product = _productReadRepository.asQueryableProduct();
            if (searchDto.CategoryId != null)
                Product = Product.Where(x => x.ProductFeatures.SubCategory.CategoryId == searchDto.CategoryId);

            if (searchDto.SubCategoryId != null)
                Product = Product.Where(x => x.ProductFeatures.SubCategoryId == searchDto.SubCategoryId);

            if (searchDto.CityId != null)
                Product = Product.Where(x => x.ProductFeatures.CityId == searchDto.CityId);

            if (searchDto.ProductName != null)
                Product = Product.Where(i => EF.Functions.Like(i.ProductFeatures.Name, $"%{searchDto.ProductName}%"));
            var now = DateTime.UtcNow;
            Product = Product.Where(x => x.ProductFeatures.ExpirationDateVip > now);
            Product = Product.Where(x => !x.IsDelete && x.ProductFeatures.ProductStatus == ProductStatus.Active);

            return Product;
        }
        public IQueryable<Product> SearchProductPremium(SearchDto searchDto)
        {
            var Product = _productReadRepository.asQueryableProduct();
            if (searchDto.CategoryId != null)
                Product = Product.Where(x => x.ProductFeatures.SubCategory.CategoryId == searchDto.CategoryId);

            if (searchDto.SubCategoryId != null)
                Product = Product.Where(x => x.ProductFeatures.SubCategoryId == searchDto.SubCategoryId);

            if (searchDto.CityId != null)
                Product = Product.Where(x => x.ProductFeatures.CityId == searchDto.CityId);

            if (searchDto.ProductName != null)
                Product = Product.Where(i => EF.Functions.Like(i.ProductFeatures.Name, $"%{searchDto.ProductName}%"));

            var now = DateTime.UtcNow;
            Product = Product.Where(x => x.ProductFeatures.ExpirationDatePremium > now);
            Product = Product.Where(x => !x.IsDelete && x.ProductFeatures.ProductStatus == ProductStatus.Active);

            return Product;
        }
    }
}
