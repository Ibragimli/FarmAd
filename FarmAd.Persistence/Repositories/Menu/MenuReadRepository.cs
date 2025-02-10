using FarmAd.Application.Repositories.Menu;
using FarmAd.Persistence.Contexts;

namespace FarmAd.Persistence.Repositories.Menu
{
    public class MenuReadRepository : ReadRepository<FarmAd.Domain.Entities.Menu>, IMenuReadRepository
    {
        public MenuReadRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
