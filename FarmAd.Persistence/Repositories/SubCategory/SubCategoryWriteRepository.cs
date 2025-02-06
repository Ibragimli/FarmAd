using FarmAd.Application.Repositories.SubCategory;
using FarmAd.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Repositories.SubCategory
{
    public class SubCategoryWriteRepository : WriteRepository<FarmAd.Domain.Entities.SubCategory>, ISubCategoryWriteRepository
    {
        public SubCategoryWriteRepository(DataContext context) : base(context)
        {
        }
    }
}
