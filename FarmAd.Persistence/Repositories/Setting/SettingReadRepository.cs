using FarmAd.Application.Repositories.Setting;
using FarmAd.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Repositories.Setting
{
    public class SettingReadRepository : ReadRepository<FarmAd.Domain.Entities.Setting>, ISettingReadRepository
    {
        public SettingReadRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
