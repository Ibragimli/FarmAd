using FarmAd.Application.Repositories.Product;
using FarmAd.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Repositories.Product
{
    public class ProductUserIdReadRepository : ReadRepository<FarmAd.Domain.Entities.ProductUserId>, IProductUserIdReadRepository
    {
        public ProductUserIdReadRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
