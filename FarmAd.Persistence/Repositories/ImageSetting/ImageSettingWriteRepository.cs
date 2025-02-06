using FarmAd.Application.Repositories.ImageSetting;
using FarmAd.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Repositories.ImageSetting
{
    public class ImageSettingWriteRepository : WriteRepository<FarmAd.Domain.Entities.ImageSetting>, IImageSettingWriteRepository
    {
        public ImageSettingWriteRepository(DataContext context) : base(context)
        {
        }
    }
}
