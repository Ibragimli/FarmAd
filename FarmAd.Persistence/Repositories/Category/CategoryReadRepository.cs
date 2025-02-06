using FarmAd.Application.Repositories.Category;
using FarmAd.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Repositories.Category
{
    public class CategoryReadRepository : ReadRepository<FarmAd.Domain.Entities.Category>, ICategoryReadRepository
    {
        public CategoryReadRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
