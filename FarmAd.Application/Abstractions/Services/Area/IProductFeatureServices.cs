using FarmAd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.Area
{
    public interface IProductFeatureServices
    {
        Task<(IList<ProductFeature>, int)> GetAllFeatures(string name, int page, int size);

    }
}
