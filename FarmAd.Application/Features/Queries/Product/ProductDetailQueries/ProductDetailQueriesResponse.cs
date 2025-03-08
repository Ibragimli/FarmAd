using FarmAd.Domain.Entities;
using FarmAd.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Queries.Product.ProductDetailQueries
{
    public class ProductDetailQueriesResponse
    {
        public FarmAd.Domain.Entities.Product Product { get; set; }
        public List<FarmAd.Domain.Entities.Product> SimilarProducts { get; set; }
        public List<FarmAd.Domain.Entities.ServiceDuration> ServiceDurations { get; set; }
        public int WishCount { get; set; }
    }
}
