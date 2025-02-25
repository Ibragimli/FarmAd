using FarmAd.Application.Abstractions.Services.Area;
using FarmAd.Application.Repositories.ProductFeature;
using FarmAd.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Services.Area
{
    public class ProductFeatureServices : IProductFeatureServices
    {
        private readonly IProductFeatureReadRepository _productFeatureReadRepository;

        public ProductFeatureServices(IProductFeatureReadRepository productFeatureReadRepository)
        {
            _productFeatureReadRepository = productFeatureReadRepository;
        }
        public async Task<(IList<ProductFeature>, int)> GetAllFeatures(string name, int page, int size)
        {
            var count = await _productFeatureReadRepository.GetTotalCountAsync(x => !x.IsDelete);
            var list = _productFeatureReadRepository.GetAllPagenated(page, size);
            if (name != null)
                list = list.Where(i => EF.Functions.Like(i.Name, $"%{name}%"));
            return (await list.ToListAsync(), count);
        }
    }
}
