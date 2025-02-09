using FarmAd.Domain.Entities;
using FarmAd.Domain.Enums;

using FarmAd.Application.DTOs.User;
using FarmAd.Application.Abstractions.Services.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Domain.Entities.Identity;
using FarmAd.Application.Repositories.Product;
using FarmAd.Application.Repositories.Payment;

namespace Ferma.Service.Services.Implementations.User
{
    public class ProfileIndexServices : IProfileIndexServices
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IPaymentReadRepository _paymentReadRepository;

        public ProfileIndexServices(IProductReadRepository productReadRepository, IPaymentReadRepository paymentReadRepository)
        {
            _productReadRepository = productReadRepository;
            _paymentReadRepository = paymentReadRepository;
        }
        public async Task<ProfileGetDto> _profileVM(AppUser user)
        {
            ProfileGetDto profileVM = new ProfileGetDto
            {
                User = user,
                ActiveProducts = await _productReadRepository.GetAllAsync(x => !x.IsDelete && x.ProductFeatures.ProductStatus == ProductStatus.Active && x.ProductFeatures.PhoneNumber == user.PhoneNumber, true,"ProductFeatures.City", "ProductFeatures", "ProductImages"),
                DeactiveProducts = await _productReadRepository.GetAllAsync(x => !x.IsDelete && x.ProductFeatures.ProductStatus == ProductStatus.DeActive && x.ProductFeatures.PhoneNumber == user.PhoneNumber, true, "ProductFeatures.City", "ProductFeatures", "ProductImages"),
                WaitedProducts = await _productReadRepository.GetAllAsync(x => !x.IsDelete && x.ProductFeatures.ProductStatus == ProductStatus.Waiting && x.ProductFeatures.PhoneNumber == user.PhoneNumber,true, "ProductFeatures.City", "ProductFeatures", "ProductImages"),
                DisabledProducts = await _productReadRepository.GetAllAsync(x => !x.IsDelete && x.ProductFeatures.ProductStatus == ProductStatus.Disabled && x.ProductFeatures.PhoneNumber == user.PhoneNumber,true, "ProductFeatures.City", "ProductFeatures", "ProductImages"),
                PersonalPayments = await _paymentReadRepository.GetAllAsync(x => !x.IsDelete && x.Service == PaymentService.BalancePayment && x.AppUserId == user.Id),
                ProductPayments = await _paymentReadRepository.GetAllAsync(x => !x.IsDelete && x.Service == PaymentService.ProductPayment && x.AppUserId == user.Id, true,"AppUser", "Products.ProductFeatures"),
                ProfileEditDto = new ProfileEditDto(),
            };
            return profileVM;
        }
    }
}
