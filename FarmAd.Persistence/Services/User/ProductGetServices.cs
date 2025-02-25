using FarmAd.Application.Abstractions.Services.User;
using FarmAd.Application.Repositories.Product;
using FarmAd.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Services.User
{
    public class ProductGetServices : IProductGetServices
    {
        private readonly IProductReadRepository _productReadRepository;

        public ProductGetServices(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }
        public async Task<(IList<Product>, int)> Products(string name, int page, int size)
        {
            var count = await _productReadRepository.GetTotalCountAsync(x => !x.IsDelete);
            var list = _productReadRepository.GetAllPagenated(page, size, true, "ProductFeatures");
            if (name != null)
                list = list.Where(i => EF.Functions.Like(i.ProductFeatures.Name, $"%{name}%"));
            return (await list.ToListAsync(), count);
        }
    }
}
