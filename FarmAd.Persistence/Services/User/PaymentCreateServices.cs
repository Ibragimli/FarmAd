using FarmAd.Domain.Entities;
using FarmAd.Domain.Enums;

using FarmAd.Application.Exceptions;
using FarmAd.Application.DTOs.User;
using FarmAd.Application.Abstractions.Services.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Application.Repositories.Product;
using FarmAd.Infrastructure.Service.User;
using FarmAd.Application.Repositories.ServiceDuration;
using FarmAd.Application.Repositories.Payment;

namespace FarmAd.Persistence.Services.User
{
    public class PaymentCreateServices : IPaymentCreateServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPaymentWriteRepository _paymentWriteRepository;
        private readonly IServiceDurationReadRepository _serviceDurationReadRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IUserService _userService;

        public PaymentCreateServices(IHttpContextAccessor httpContextAccessor, IPaymentWriteRepository paymentWriteRepository, IServiceDurationReadRepository serviceDurationReadRepository, IProductReadRepository productReadRepository, IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _paymentWriteRepository = paymentWriteRepository;
            _serviceDurationReadRepository = serviceDurationReadRepository;
            _productReadRepository = productReadRepository;
            _userService = userService;
        }
        public async Task PaymentCheck(PaymentCreateDto paymentCreateDto)
        {

            bool ValidateProduct = await _productReadRepository.IsExistAsync(paymentCreateDto.ProductId);
            if (!ValidateProduct)
                throw new NotFoundException("Notfound");
            if (paymentCreateDto.ProductStatus != ProductStatus.Active)
                throw new NotFoundException("Notfound");
            if (paymentCreateDto.AppUserId != null)
            {
                bool userCheck = await _userService.IsExistAsync(paymentCreateDto.AppUserId);
                if (!userCheck)
                    throw new NotFoundException("Notfound");
            }
            bool durationCheck = await _serviceDurationReadRepository.IsExistAsync(paymentCreateDto.DurationServicesId);
            if (!durationCheck)
                throw new NotFoundException("Notfound");
            if (paymentCreateDto.Services == 0)
                throw new NotFoundException("Notfound");
            if (paymentCreateDto.Source == 0)
                throw new NotFoundException("Notfound");
            if (paymentCreateDto.ServiceType == 0)
                throw new NotFoundException("Notfound");
        }
        public async Task PaymentCreateBalance(PaymentCreateDto paymentCreateDto)
        {
            if (paymentCreateDto.Source != Source.Balance)
                throw new NotFoundException("Notfound");
            if (paymentCreateDto.AppUserId == null)
                throw new NotFoundException("Notfound");
            var user = await _userService.GetUserAsync(paymentCreateDto.AppUserId);
            if (user == null)
                throw new NotFoundException("Notfound");

            var duration = await _serviceDurationReadRepository.GetByIdAsync(paymentCreateDto.DurationServicesId);

            if (user.Balance < duration.Amount)
            {
                throw new PaymentValueException("Balansınızda kifayət qədər məbləğ yoxdur!");
            }
            var Product = await _productReadRepository.GetByIdAsync(paymentCreateDto.ProductId, true, "ProductFeatures");

            Payment payment = new Payment
            {
                Duration = duration.Duration,
                Amount = duration.Amount,
                AppUserId = paymentCreateDto.AppUserId,
                ProductId = paymentCreateDto.ProductId,
                Service = paymentCreateDto.Services,
                ServiceType = paymentCreateDto.ServiceType,
                Source = paymentCreateDto.Source,

            };
            await _paymentWriteRepository.AddAsync(payment);
            if (paymentCreateDto.ServiceType == ServiceType.Vip)
            {
                Product.ProductFeatures.IsVip = true;
                Product.ProductFeatures.ExpirationDateVip = DateTime.Now.AddDays(duration.Duration);
            }
            else
            {
                Product.ProductFeatures.IsPremium = true;
                Product.ProductFeatures.ExpirationDatePremium = DateTime.Now.AddDays(duration.Duration);
            }
            user.Balance = user.Balance - duration.Amount;
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("paymentDto");
            await _paymentWriteRepository.SaveAsync();
        }
        public async Task PaymentCreateBankCard(PaymentCreateDto paymentCreateDto)
        {
            var duration = await _serviceDurationReadRepository.GetByIdAsync(paymentCreateDto.DurationServicesId);

            var Product = await _productReadRepository.GetByIdAsync(paymentCreateDto.ProductId, true, "ProductFeatures");
            Payment payment = new Payment
            {
                Duration = duration.Duration,
                Amount = duration.Amount,
                AppUserId = paymentCreateDto.AppUserId,
                ProductId = paymentCreateDto.ProductId,
                Service = paymentCreateDto.Services,
                ServiceType = paymentCreateDto.ServiceType,
                Source = paymentCreateDto.Source,

            };
            await _paymentWriteRepository.AddAsync(payment);
            if (paymentCreateDto.ServiceType == ServiceType.Vip)
            {
                Product.ProductFeatures.IsVip = true;
                Product.ProductFeatures.ExpirationDateVip = DateTime.Now.AddDays(duration.Duration);
            }
            else
            {
                Product.ProductFeatures.IsPremium = true;
                Product.ProductFeatures.ExpirationDatePremium = DateTime.Now.AddDays(duration.Duration);
            }
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("paymentDto");

            await _paymentWriteRepository.SaveAsync();
        }
    }
}
