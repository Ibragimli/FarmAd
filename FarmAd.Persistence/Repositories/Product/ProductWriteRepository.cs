using FarmAd.Application.Repositories.Product;
using FarmAd.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Repositories.Product
{
    public class ProductWriteRepository : WriteRepository<FarmAd.Domain.Entities.Product>, IProductWriteRepository
    {
        public ProductWriteRepository(DataContext context) : base(context)
        {
        }
    }
}
