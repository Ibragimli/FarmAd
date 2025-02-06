using FarmAd.Application.Repositories.Product;
using FarmAd.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Repositories.Product
{
    public class ProductReadRepository : ReadRepository<FarmAd.Domain.Entities.Product>, IProductReadRepository
    {
        public ProductReadRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
