using FarmAd.Application.Repositories.Payment;
using FarmAd.Domain.Entities;
using FarmAd.Domain.Enums;

using Ferma.Service.Services.Interfaces.Area;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ferma.Service.Services.Implementations.Area
{
    public class BalancePaymentIndexServices : IBalancePaymentIndexServices
    {
        private readonly IPaymentReadRepository _paymentReadRepository;

        public BalancePaymentIndexServices(IPaymentReadRepository paymentReadRepository)
        {
            _paymentReadRepository = paymentReadRepository;
        }
        public IQueryable<Payment> GetPayments(int year, int month)
        {
            var payments = _paymentReadRepository.AsQueryable("AppUser", "Products.ProductFeatures");
            payments = payments.Where(x => !x.IsDelete);
            payments = payments.Where(x => x.Service == PaymentService.BalancePayment);
            if (year != 0)
                payments = payments.Where(x => x.CreatedDate.Year == year);
            if (month != 0)
                payments = payments.Where(x => x.CreatedDate.Month == month);

            return payments;
        }
    }
}
