using FarmAd.Application.Repositories.Category;
using FarmAd.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Repositories.Category
{
    public class CategoryWriteRepository : WriteRepository<FarmAd.Domain.Entities.Category>, ICategoryWriteRepository
    {
        public CategoryWriteRepository(DataContext context) : base(context)
        {
        }
    }
}
