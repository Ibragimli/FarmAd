using FarmAd.Application.Repositories.ProductImage;
using FarmAd.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Repositories.ProductImage
{
    public class ProductImageWriteRepository : WriteRepository<FarmAd.Domain.Entities.ProductImage>, IProductImageWriteRepository
    {
        public ProductImageWriteRepository(DataContext context) : base(context)
        {
        }
    }
}
