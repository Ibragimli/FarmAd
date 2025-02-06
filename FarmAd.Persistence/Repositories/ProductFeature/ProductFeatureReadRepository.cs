using FarmAd.Application.Repositories.ProductFeature;
using FarmAd.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Repositories.ProductFeature
{
    public class ProductFeatureReadRepository : ReadRepository<FarmAd.Domain.Entities.ProductFeature>, IProductFeatureReadRepository
    {
        public ProductFeatureReadRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
