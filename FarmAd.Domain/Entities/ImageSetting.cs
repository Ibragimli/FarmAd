using FarmAd.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Domain.Entities
{
    public class ImageSetting : BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }

    }
}
