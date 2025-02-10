using FarmAd.Domain.Entities;

using FarmAd.Application.Exceptions;
using FarmAd.Application.Abstractions.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Application.Repositories.Product;
using FarmAd.Application.Repositories.WishItem;
using Microsoft.Extensions.DependencyInjection;
using FarmAd.Application.Repositories.ServiceDuration;

namespace FarmAd.Persistence.Service.User
{
    public class ProductDetailIndexServices : IProductDetailIndexServices
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IServiceDurationReadRepository _serviceDurationReadRepository;
        private readonly IWishItemReadRepository _wishItemReadRepository;
        private readonly IProductUserIdReadRepository _productUserIdReadRepository;

        public ProductDetailIndexServices(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IServiceDurationReadRepository serviceDurationReadRepository, IWishItemReadRepository wishItemReadRepository, IProductUserIdReadRepository productUserIdReadRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _serviceDurationReadRepository = serviceDurationReadRepository;
            _wishItemReadRepository = wishItemReadRepository;
            _productUserIdReadRepository = productUserIdReadRepository;
        }


        public async Task<Product> GetProduct(int id)
        {
            var Product = await _productReadRepository.GetAsync(x => x.Id == id && !x.IsDelete, "ProductImages", "ProductUserIds.AppUser", "ProductFeatures.City", "ProductFeatures.SubCategory.Category");

            if (Product == null) throw new NotFoundException("error444");
            return Product;
        }
        public async Task<List<Product>> GetSimilarProduct(int id, Product Product)
        {
            var similarProduct = await _productReadRepository.GetAllAsync(x => x.Id != id && !x.IsDelete && x.ProductFeatures.SubCategory.CategoryId == Product.ProductFeatures.SubCategory.CategoryId, true,
                   "ProductImages", "ProductUserIds.AppUser", "ProductFeatures.City", "ProductFeatures.SubCategory.Category");

            return similarProduct.ToList();
        }

        public async Task<ProductUserId> GetUser(int id)
        {
            var user = await _productUserIdReadRepository.GetAsync(x => x.ProductId == id && !x.IsDelete);
            if (user == null) throw new NotFoundException("error444");

            return user;
        }

        public async Task<int> GetWishCount(int id)
        {
            var count = await _wishItemReadRepository.GetTotalCountAsync(x => x.ProductId == id && !x.IsDelete);

            return count;
        }
        public async Task<List<ServiceDuration>> GetServiceDurations()
        {
            var durations = await _serviceDurationReadRepository.GetAllAsync(x => !x.IsDelete);
            return durations.ToList();
        }
        public async Task ProductViewCount(Product Product)
        {
            Product.ProductFeatures.ViewCount++;
            await _productWriteRepository.SaveAsync();
        }
    }
}
