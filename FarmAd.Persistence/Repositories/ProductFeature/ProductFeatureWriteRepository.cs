using FarmAd.Application.Repositories.ProductFeature;
using FarmAd.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Repositories.ProductFeature
{
    public class ProductFeatureWriteRepository : WriteRepository<FarmAd.Domain.Entities.ProductFeature>, IProductFeatureWriteRepository
    {
        public ProductFeatureWriteRepository(DataContext context) : base(context)
        {
        }
    }
}
