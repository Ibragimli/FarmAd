using FarmAd.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Domain.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<ProductFeature> ProductFeatures { get; set; }

    }
}
