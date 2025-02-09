using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Repositories.Product
{
    public interface IProductReadRepository : IReadRepository<FarmAd.Domain.Entities.Product>
    {
        public IQueryable<FarmAd.Domain.Entities.Product> asQueryableProduct();

    }
}
