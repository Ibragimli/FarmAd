using FarmAd.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.User
{
    public interface IPaymentCreateServices
    {
        public Task PaymentCheck(PaymentCreateDto paymentCreateDto);
        public Task PaymentCreateBankCard(PaymentCreateDto paymentCreateDto);
        public Task PaymentCreateBalance(PaymentCreateDto paymentCreateDto);
    }
}
