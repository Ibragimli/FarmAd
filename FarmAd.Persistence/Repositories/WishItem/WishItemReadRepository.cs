using FarmAd.Application.Repositories.WishItem;
using FarmAd.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Repositories.WishItem
{
    public class WishItemReadRepository : ReadRepository<FarmAd.Domain.Entities.WishItem>, IWishItemReadRepository
    {
        public WishItemReadRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
