using FarmAd.Application.Repositories.Setting;
using FarmAd.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Repositories.Setting
{
    public class SettingWriteRepository : WriteRepository<FarmAd.Domain.Entities.Setting>, ISettingWriteRepository
    {
        public SettingWriteRepository(DataContext context) : base(context)
        {
        }
    }
}
