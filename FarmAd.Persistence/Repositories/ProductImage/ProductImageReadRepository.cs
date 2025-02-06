using FarmAd.Application.Repositories.ProductImage;
using FarmAd.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Repositories.ProductImage
{
    public class ProductImageReadRepository : ReadRepository<FarmAd.Domain.Entities.ProductImage>, IProductImageReadRepository
    {
        public ProductImageReadRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
