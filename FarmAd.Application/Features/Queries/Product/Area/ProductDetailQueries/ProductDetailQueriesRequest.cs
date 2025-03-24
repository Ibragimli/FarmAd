using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Queries.Product.Area.ProductDetailQueries
{
    public class ProductDetailQueriesRequest : IRequest<ProductDetailQueriesResponse>
    {
        public int Id { get; set; }
    }
}
