using FarmAd.Domain.Entities;
using FarmAd.Domain.Enums;
using FarmAd.Application.Exceptions;
using FarmAd.Application.DTOs.User;
using FarmAd.Application.Abstractions.Services.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Domain.Entities.Identity;
using FarmAd.Infrastructure.Service.User;
using FarmAd.Application.Repositories.Payment;

namespace FarmAd.Persistence.Service.User
{
    public class BalanceIncreaseServices : IBalanceIncreaseServices
    {
        private readonly IUserService _userService;
        private readonly IPaymentWriteRepository _paymentWriteRepository;

        public BalanceIncreaseServices(IUserService userService, IPaymentWriteRepository paymentWriteRepository)
        {
            _userService = userService;
            _paymentWriteRepository = paymentWriteRepository;
        }
        public async Task BalanceIncrease(BalanceDto balanceDto)
        {
            AppUser user = await _userService.GetUserIdAsync(balanceDto.AppUserId);
            user.Balance += balanceDto.Balance;

            Payment payment = new Payment
            {
                Amount = balanceDto.Balance,
                AppUserId = balanceDto.AppUserId,
                Service = PaymentService.BalancePayment,
                Source = Source.BankCard,
            };
            await _paymentWriteRepository.AddAsync(payment);
            await _paymentWriteRepository.SaveAsync();
        }

        public async Task CheckBalanceIncrease(BalanceDto balanceDto)
        {
            if (balanceDto.AppUserId == null)
                throw new UserNotFoundException("");
            AppUser user = await _userService.GetUserIdAsync(balanceDto.AppUserId);

            if (user == null)
                throw new UserNotFoundException("");
            if (balanceDto.Source != Source.BankCard)
                balanceDto.Source = Source.BankCard;
            if (balanceDto.Balance < 1 || balanceDto.Balance > 1000)
                throw new PaymentValueException("Balansınızı 0 və 1000 AZN aralığında artıra bilərsinz!");

        }
    }
}
