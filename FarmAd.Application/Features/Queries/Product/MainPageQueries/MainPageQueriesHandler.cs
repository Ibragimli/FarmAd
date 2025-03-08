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

namespace FarmAd.Application.Features.Queries.Product.MainPageQueries
{
    public class MainPageQueriesHandler : IRequestHandler<MainPageQueriesRequest, MainPageQueriesResponse>
    {
        private readonly IProductGetServices _productGetServices;

        public MainPageQueriesHandler(IProductGetServices productGetServices)
        {
            _productGetServices = productGetServices;
        }
        public async Task<MainPageQueriesResponse> Handle(MainPageQueriesRequest request, CancellationToken cancellationToken)
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
