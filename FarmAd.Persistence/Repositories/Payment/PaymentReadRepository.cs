using FarmAd.Application.Repositories.Payment;
using FarmAd.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Repositories.Payment
{
    public class PaymentReadRepository : ReadRepository<FarmAd.Domain.Entities.Payment>, IPaymentReadRepository
    {
        public PaymentReadRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
