using FarmAd.Application.Repositories.Product;
using FarmAd.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Repositories.Product
{
    public class ProductUserIdWriteRepository : WriteRepository<FarmAd.Domain.Entities.ProductUserId>, IProductUserIdWriteRepository
    {
        public ProductUserIdWriteRepository(DataContext context) : base(context)
        {
        }
    }
}
