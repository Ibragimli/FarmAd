using FarmAd.Domain.Entities;
using FarmAd.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Queries.ProductFeature.GetAllFeatures
{
    public class GetAllFeaturesResponse
    {
        public List<FarmAd.Domain.Entities.ProductFeature> Datas { get; set; }
        public int TotalCount { get; set; }
    }
}
