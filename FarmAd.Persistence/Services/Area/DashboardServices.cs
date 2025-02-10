using FarmAd.Application.Repositories.ContactUs;
using FarmAd.Application.Repositories.Payment;
using FarmAd.Application.Repositories.Product;
using FarmAd.Domain.Entities;
using FarmAd.Domain.Enums;
using FarmAd.Application.Abstractions.Services.Area;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Service.Area
{
    public class DashboardServices : IDashboardServices
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IPaymentReadRepository _paymentReadRepository;
        private readonly IContactUsReadRepository _contactUsReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public DashboardServices(IProductReadRepository productReadRepository, IPaymentReadRepository paymentReadRepository, IContactUsReadRepository contactUsReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _paymentReadRepository = paymentReadRepository;
            _contactUsReadRepository = contactUsReadRepository;
            _productWriteRepository = productWriteRepository;
        }
        public async Task IsActive()
        {
            var now = DateTime.UtcNow.AddHours(4);
            var ValidateProduct = await _productReadRepository.IsExistAsync(x => !x.IsDelete && x.ProductFeatures.ProductStatus == ProductStatus.Active && x.ProductFeatures.ExpirationDateActive < now, true, "ProductFeatures");
            if (ValidateProduct)
            {
                var Products = await _productReadRepository.GetAllAsync(x => !x.IsDelete && x.ProductFeatures.ProductStatus == ProductStatus.Active && x.ProductFeatures.ExpirationDateActive < now, true, "ProductFeatures");
                foreach (var Product in Products)
                {
                    Product.ProductFeatures.ProductStatus = ProductStatus.DeActive;
                    await _productWriteRepository.SaveAsync();
                }
            }
        }
        public async Task<int> AllProductCount()
        {
            var count = await _productReadRepository.GetTotalCountAsync(x => !x.IsDelete && x.ProductFeatures.ProductStatus == ProductStatus.Active, true, "ProductFeatures");
            return count;
        }


        public async Task<int> NewContactCount()
        {
            var now = DateTime.UtcNow.AddHours(4).AddDays(5);
            var count = await _contactUsReadRepository.GetTotalCountAsync(x => !x.IsDelete && x.CreatedDate < now);
            return count;
        }

        public async Task<int> NewProductCount()
        {
            var count = await _productReadRepository.GetTotalCountAsync(x => !x.IsDelete && x.ProductFeatures.ProductStatus == ProductStatus.Waiting, true, "ProductFeatures");
            return count;
        }

        public async Task<decimal?> PaymentMoney()
        {
            decimal? money = 0;
            var payments = await _paymentReadRepository.GetAllAsync(x => !x.IsDelete && x.Source != Source.Balance);
            foreach (var item in payments)
            {

                money += item.Amount;
            }
            return money;
        }

        public async Task<List<Payment>> Payments()
        {
            var payments = await _paymentReadRepository.GetAllAsync(x => !x.IsDelete);
            return payments.Take(8).ToList();
        }

        public async Task<int> PremiumProductCount()
        {
            var now = DateTime.UtcNow.AddHours(4);
            var count = await _productReadRepository.GetTotalCountAsync(x => !x.IsDelete && x.ProductFeatures.ProductStatus == ProductStatus.Active && x.ProductFeatures.ExpirationDatePremium > now, true, "ProductFeatures");
            return count;
        }

        public async Task<int> VipProductCount()
        {
            var now = DateTime.UtcNow.AddHours(4);
            var count = await _productReadRepository.GetTotalCountAsync(x => !x.IsDelete && x.ProductFeatures.ProductStatus == ProductStatus.Active && x.ProductFeatures.ExpirationDateVip > now, true, "ProductFeatures");
            return count;
        }
    }
}
