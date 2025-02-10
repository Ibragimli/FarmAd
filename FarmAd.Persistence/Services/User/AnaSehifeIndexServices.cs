using FarmAd.Application.Abstractions.Services.User;
using FarmAd.Application.Repositories.Category;
using FarmAd.Application.Repositories.Product;
using FarmAd.Domain.Entities;
using FarmAd.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Services.User
{
    public class AnaSehifeIndexServices : IAnaSehifeIndexServices
    {
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly IProductReadRepository _productReadRepository;

        public AnaSehifeIndexServices(ICategoryReadRepository categoryReadRepository, IProductReadRepository productReadRepository)
        {
            _categoryReadRepository = categoryReadRepository;
            _productReadRepository = productReadRepository;
        }

        public async Task<List<Category>> GetAllCategoryAsync()
        {
            var categories = await _categoryReadRepository.GetAllAsync(x => !x.IsDelete);
            return categories.ToList();
        }

        public IQueryable<Product> GetProductsAsync()
        {
            var Products = _productReadRepository.AsQueryable();
            Products = Products.Where(x => !x.IsDelete && x.ProductFeatures.ProductStatus == ProductStatus.Active);
            return Products;
        }
        public IQueryable<Product> GetVipProductAsync()
        {
            var now = DateTime.Now;
            var Products = _productReadRepository.AsQueryable().Where(x => x.ProductFeatures.ExpirationDateVip > now);
            Products = Products.Where(x => !x.IsDelete && x.ProductFeatures.ProductStatus == ProductStatus.Active);
            return Products;
        }
        public IQueryable<Product> GetPremiumProductAsync()
        {
            var now = DateTime.Now;
            var Products = _productReadRepository.AsQueryable().Where(x => x.ProductFeatures.ExpirationDatePremium > now);
            Products = Products.Where(x => !x.IsDelete && x.ProductFeatures.ProductStatus == ProductStatus.Active);

            return Products;
        }
        public IQueryable<Product> GetPreProductAsync()
        {
            var now = DateTime.Now;
            var Products = _productReadRepository.AsQueryable().Where(x => x.ProductFeatures.ExpirationDatePremium > now || x.ProductFeatures.ExpirationDatePremium > now);
            Products = Products.Where(x => !x.IsDelete && x.ProductFeatures.ProductStatus == ProductStatus.Active);
            return Products;
        }

    }
}
