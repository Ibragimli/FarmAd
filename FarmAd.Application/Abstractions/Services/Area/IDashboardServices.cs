using FarmAd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.Area
{
    public interface IDashboardServices
    {
        public Task<int> AllProductCount();
        public Task<int> VipProductCount();
        public Task<int> PremiumProductCount();
        public Task<int> NewProductCount();
        public Task<int> NewContactCount();
        public Task<decimal?> PaymentMoney();
        public Task<List<Payment>> Payments();
        public Task IsActive();

    }
}
