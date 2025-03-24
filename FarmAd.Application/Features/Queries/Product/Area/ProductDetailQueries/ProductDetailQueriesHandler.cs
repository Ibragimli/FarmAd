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

namespace FarmAd.Application.Features.Queries.Product.Area.ProductDetailQueries
{
    public class ProductDetailQueriesHandler : IRequestHandler<ProductDetailQueriesRequest, ProductDetailQueriesResponse>
    {
        private readonly IAdminProductDetailIndexServices _adminProductDetailIndexServices;

        public ProductDetailQueriesHandler(IAdminProductDetailIndexServices adminProductDetailIndexServices)
        {
            _adminProductDetailIndexServices = adminProductDetailIndexServices;
        }
        public async Task<ProductDetailQueriesResponse> Handle(ProductDetailQueriesRequest request, CancellationToken cancellationToken)
        {
            var prd = await _adminProductDetailIndexServices.GetProduct(request.Id);
            var productUserId = await _adminProductDetailIndexServices.GetAppUser(request.Id);
            var cities = await _adminProductDetailIndexServices.GetAllCities();
            var categories = await _adminProductDetailIndexServices.GetCategories();
            var subCategories = await _adminProductDetailIndexServices.GetSubCategories();

            return new()
            {
                Product = prd,
                ProductUserId = productUserId,
                Cities = cities,
                Categories = categories,
                Subcategories = subCategories
            };

        }
    }
}
