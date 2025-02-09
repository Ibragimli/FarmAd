using FarmAd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ferma.Service.Services.Interfaces.Area
{
    public interface IBalancePaymentIndexServices
    {
        public IQueryable<Payment> GetPayments(int year, int month);
    }
}
