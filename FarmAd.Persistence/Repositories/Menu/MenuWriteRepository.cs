using FarmAd.Application.Repositories.Menu;
using FarmAd.Persistence.Contexts;

namespace FarmAd.Persistence.Repositories.Menu
{
    public class MenuWriteRepository : WriteRepository<FarmAd.Domain.Entities.Menu>, IMenuWriteRepository
    {
        public MenuWriteRepository(DataContext context) : base(context)
        {
        }
    }
}
