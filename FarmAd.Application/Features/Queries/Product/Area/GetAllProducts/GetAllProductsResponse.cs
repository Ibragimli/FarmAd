using FarmAd.Domain.Entities;
using FarmAd.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Queries.Product.Area.GetAllProducts
{
    public class GetAllProductsResponse
    {
        public List<FarmAd.Domain.Entities.Product> Datas { get; set; }
        public List<FarmAd.Domain.Entities.Category> Categories { get; set; }
        public List<FarmAd.Domain.Entities.SubCategory> SubCategories { get; set; }
        public int TotalCount { get; set; }
    }
}
