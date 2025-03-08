using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.Area;
using FarmAd.Application.Abstractions.Services.User;
using FarmAd.Application.Repositories.City;
using FarmAd.Domain.Entities.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Queries.Product.ProductDetailQueries
{
    public class ProductDetailQueriesHandler : IRequestHandler<ProductDetailQueriesRequest, ProductDetailQueriesResponse>
    {
        private readonly IProductDetailIndexServices _productDetailIndexServices;

        public ProductDetailQueriesHandler(IProductDetailIndexServices productDetailIndexServices)
        {
            _productDetailIndexServices = productDetailIndexServices;
        }
        public async Task<ProductDetailQueriesResponse> Handle(ProductDetailQueriesRequest request, CancellationToken cancellationToken)
        {
            var prd = await _productDetailIndexServices.GetProduct(request.Id);
            var similarPrds = await _productDetailIndexServices.GetSimilarProduct(request.Id, prd);
            var servideDurations = await _productDetailIndexServices.GetServiceDurations();
            var wishCount = await _productDetailIndexServices.GetWishCount(request.Id);
            await _productDetailIndexServices.ProductViewCount(prd);

            return new()
            {
                Product = prd,
                WishCount = wishCount,
                ServiceDurations = servideDurations,
                SimilarProducts = similarPrds,
            };

        }
    }
}
