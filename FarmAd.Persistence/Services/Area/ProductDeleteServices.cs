using FarmAd.Domain.Entities;

using FarmAd.Application.Exceptions;
using FarmAd.Application.Abstractions.Services.Area;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Repositories.Product;
using FarmAd.Application.Repositories.ProductImage;
using FarmAd.Application.Repositories.Payment;
using FarmAd.Application.Repositories.WishItem;
using FarmAd.Application.Repositories.ProductFeature;

namespace FarmAd.Persistence.Service.Area
{
    public class ProductDeleteServices : IProductDeleteServices
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IWishItemWriteRepository _wishItemWriteRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductFeatureWriteRepository _productFeatureWriteRepository;
        private readonly IProductUserIdWriteRepository _productUserIdWriteRepository;
        private readonly IPaymentWriteRepository _paymentWriteRepository;
        private readonly IProductImageWriteRepository _productImageWriteRepository;
        private readonly IProductUserIdReadRepository _productUserIdReadRepository;
        private readonly IProductFeatureReadRepository _productFeatureReadRepository;
        private readonly IPaymentReadRepository _paymentReadRepository;
        private readonly IWishItemReadRepository _wishItemReadRepository;
        private readonly IProductImageReadRepository _productImageReadRepository;
        private readonly IImageManagerService _manageImageHelper;

        public ProductDeleteServices(IProductReadRepository productReadRepository, IWishItemWriteRepository wishItemWriteRepository, IProductWriteRepository productWriteRepository, IProductFeatureWriteRepository productFeatureWriteRepository, IProductUserIdWriteRepository productUserIdWriteRepository, IPaymentWriteRepository paymentWriteRepository, IProductImageWriteRepository productImageWriteRepository, IProductUserIdReadRepository productUserIdReadRepository, IProductFeatureReadRepository productFeatureReadRepository, IPaymentReadRepository paymentReadRepository, IWishItemReadRepository wishItemReadRepository, IProductImageReadRepository productImageReadRepository, IImageManagerService manageImageHelper)
        {
            _productReadRepository = productReadRepository;
            _wishItemWriteRepository = wishItemWriteRepository;
            _productWriteRepository = productWriteRepository;
            _productFeatureWriteRepository = productFeatureWriteRepository;
            _productUserIdWriteRepository = productUserIdWriteRepository;
            _paymentWriteRepository = paymentWriteRepository;
            _productImageWriteRepository = productImageWriteRepository;
            _productUserIdReadRepository = productUserIdReadRepository;
            _productFeatureReadRepository = productFeatureReadRepository;
            _paymentReadRepository = paymentReadRepository;
            _wishItemReadRepository = wishItemReadRepository;
            _productImageReadRepository = productImageReadRepository;
            _manageImageHelper = manageImageHelper;
        }
        public async Task DeleteProduct(int id)
        {
            bool check = false;
            var Product = await _productReadRepository.GetAsync(x => !x.IsDelete && x.Id == id);
            if (Product == null)
                throw new ItemNotFoundException("404");
            var images = await _productImageReadRepository.GetAllAsync(x => x.ProductId == Product.Id && !x.IsDelete);
            var payments = await _paymentReadRepository.GetAllAsync(x => !x.IsDelete && x.ProductId == Product.Id);
            var ProductUserIds = await _productUserIdReadRepository.GetAllAsync(x => !x.IsDelete && x.ProductId == Product.Id);
            var wishItems = await _wishItemReadRepository.GetAllAsync(x => !x.IsDelete && x.ProductId == Product.Id);
            var feature = await _productFeatureReadRepository.GetAsync(x => x.Id == Product.ProductFeatureId && !x.IsDelete);

            if (images != null)
            {
                foreach (var image in images)
                {
                    _productImageWriteRepository.Remove(image);
                    _manageImageHelper.DeleteFile(image.Image, "Product");
                }
                check = true;
            }
            if (payments != null)
            {
                foreach (var payment in payments)
                {
                    _paymentWriteRepository.Remove(payment);
                }
                check = true;

            }
            if (ProductUserIds != null)
            {
                foreach (var ProductUserId in ProductUserIds)
                {
                    _productUserIdWriteRepository.Remove(ProductUserId);
                }
                check = true;
            }
            if (wishItems != null)
            {
                foreach (var wishItem in wishItems)
                {
                    _wishItemWriteRepository.Remove(wishItem);
                }
                check = true;
            }
            if (check)
            {
                _productWriteRepository.Remove(Product);
                _productFeatureWriteRepository.Remove(feature);
                await _productWriteRepository.SaveAsync();
            }

        }
    }
}
