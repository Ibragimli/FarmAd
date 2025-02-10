using FarmAd.Domain.Entities;
using FarmAd.Domain.Enums;

using FarmAd.Application.Abstractions.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarmAd.Application.Repositories.Product;

namespace FarmAd.Persistence.Services.User
{
    public class UserProductsServices : IUserProductsServices
    {
        private readonly IProductReadRepository _productReadRepository;

        public UserProductsServices(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }
        public IQueryable<Product> AllProducts(string phoneNumber)
        {
            var Product = _productReadRepository.asQueryableProduct().Where(x => x.ProductFeatures.PhoneNumber == phoneNumber);
            Product = Product.Where(x => !x.IsDelete && x.ProductFeatures.ProductStatus == ProductStatus.Active);

            return Product;
        }

        public IQueryable<Product> VipProducts(string phoneNumber)
        {
            DateTime now = DateTime.UtcNow;
            var Product = _productReadRepository.asQueryableProduct().Where(x => x.ProductFeatures.PhoneNumber == phoneNumber);
            Product = Product.Where(x => x.ProductFeatures.ExpirationDateVip > now);
            Product = Product.Where(x => !x.IsDelete && x.ProductFeatures.ProductStatus == ProductStatus.Active);
            return Product;
        }
        public IQueryable<Product> PremiumProducts(string phoneNumber)
        {
            DateTime now = DateTime.UtcNow;
            var Product = _productReadRepository.asQueryableProduct().Where(x => x.ProductFeatures.PhoneNumber == phoneNumber);
            Product = Product.Where(x => x.ProductFeatures.ExpirationDatePremium > now);
            Product = Product.Where(x => !x.IsDelete && x.ProductFeatures.ProductStatus == ProductStatus.Active);
            return Product;
        }
    }
}
