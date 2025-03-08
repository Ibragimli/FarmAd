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

namespace FarmAd.Persistence.Services.User
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
            var product = await _productReadRepository.GetAsync(
                x => x.Id == id && !x.IsDelete,
                "ProductImages",
                "ProductUserIds.AppUser",
                "ProductFeatures.City",
                "ProductFeatures.SubCategory",
                "ProductFeatures.SubCategory.Category"
            );

            if (product == null) throw new NotFoundException("Elan tapılmadı!");
            return product;
        }

        public async Task<List<Product>> GetSimilarProduct(int id, Product product)
        {
            var similarProducts = await _productReadRepository.GetAllAsync(
                x => x.Id != id && !x.IsDelete && x.ProductFeatures.SubCategory.CategoryId == product.ProductFeatures.SubCategory.CategoryId,
                true,
                "ProductImages",
                "ProductUserIds.AppUser",
                "ProductFeatures.City",
                "ProductFeatures.SubCategory.Category"
            );

            return similarProducts.ToList();
        }

        public async Task<int> GetWishCount(int id)
        {
            return await _wishItemReadRepository.GetTotalCountAsync(x => x.ProductId == id && !x.IsDelete);
        }

        public async Task<List<ServiceDuration>> GetServiceDurations()
        {
            var durations = await _serviceDurationReadRepository.GetAllAsync(x => !x.IsDelete);
            return durations.ToList();
        }

        public async Task ProductViewCount(Product product)
        {
            product.ProductFeatures.ViewCount++;
            _productWriteRepository.Update(product);
            await _productWriteRepository.SaveAsync();
        }

    }
}
