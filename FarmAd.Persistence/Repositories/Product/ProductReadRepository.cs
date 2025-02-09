using FarmAd.Application.Repositories.Product;
using FarmAd.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Repositories.Product
{
    public class ProductReadRepository : ReadRepository<FarmAd.Domain.Entities.Product>, IProductReadRepository
    {
        private readonly DataContext _dbContext;

        public ProductReadRepository(DataContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<FarmAd.Domain.Entities.Product> asQueryableProduct()
        {
            var posters = _dbContext.Products
                .Include(x => x.ProductImages)
                .Include(x => x.ProductFeatures)
                .ThenInclude(x => x.City)
                .Include(x => x.ProductFeatures)
                .ThenInclude(x => x.SubCategory)
                .ThenInclude(x => x.Category)
                .AsQueryable();
            return posters;
        }
    }
}
