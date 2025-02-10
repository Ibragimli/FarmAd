using FarmAd.Application.Repositories.Payment;
using FarmAd.Domain.Entities;
using FarmAd.Domain.Enums;

using FarmAd.Application.Abstractions.Services.Area;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmAd.Persistence.Service.Area
{
    public class ProductPaymentIndexServices : IProductPaymentIndexServices
    {
        private readonly IPaymentReadRepository _paymentReadRepository;

        public ProductPaymentIndexServices(IPaymentReadRepository paymentReadRepository)
        {
            _paymentReadRepository = paymentReadRepository;
        }
        public IQueryable<Payment> GetPayments(int year, int month)
        {
            var payments = _paymentReadRepository.AsQueryable("AppUser", "Products.ProductFeatures");
            payments = payments.Where(x => !x.IsDelete);
            payments = payments.Where(x => x.Service == PaymentService.ProductPayment);
            if (year != 0)
                payments = payments.Where(x => x.CreatedDate.Year == year);
            if (month != 0)
                payments = payments.Where(x => x.CreatedDate.Month == month);

            return payments;
        }
    }
}
