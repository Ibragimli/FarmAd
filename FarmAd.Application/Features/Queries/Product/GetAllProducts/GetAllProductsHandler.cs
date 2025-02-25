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

namespace FarmAd.Application.Features.Queries.Product.GetAllProducts
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsRequest, GetAllProductsResponse>
    {
        private readonly IProductGetServices _productGetServices;

        public GetAllProductsHandler(IProductGetServices productGetServices)
        {
            _productGetServices = productGetServices;
        }
        public async Task<GetAllProductsResponse> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
        {
            var (datas, count) = await _productGetServices.Products(request.Name, request.Page, request.Size);
            return new()
            {
                Datas = datas.ToList(),
                TotalCount = count
            };

        }
    }
}
