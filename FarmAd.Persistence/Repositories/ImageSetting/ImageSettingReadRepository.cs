using FarmAd.Application.Repositories.ImageSetting;
using FarmAd.Application.Repositories.ContactUs;
using FarmAd.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Repositories.ImageSetting
{
    public class ImageSettingReadRepository : ReadRepository<FarmAd.Domain.Entities.ImageSetting>, IImageSettingReadRepository
    {
        public ImageSettingReadRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
