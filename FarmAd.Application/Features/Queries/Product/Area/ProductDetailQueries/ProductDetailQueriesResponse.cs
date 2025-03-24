using FarmAd.Domain.Entities;
using FarmAd.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Queries.Product.Area.ProductDetailQueries
{
    public class ProductDetailQueriesResponse
    {
        public FarmAd.Domain.Entities.Product Product { get; set; }
        public List<FarmAd.Domain.Entities.Category> Categories { get; set; }
        public List<FarmAd.Domain.Entities.SubCategory> Subcategories { get; set; }
        public List<FarmAd.Domain.Entities.City> Cities { get; set; }
        public FarmAd.Domain.Entities.ProductUserId ProductUserId { get; set; }
    }
}
