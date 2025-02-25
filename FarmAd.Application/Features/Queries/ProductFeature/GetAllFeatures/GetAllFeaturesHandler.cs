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

namespace FarmAd.Application.Features.Queries.ProductFeature.GetAllFeatures
{
    public class GetAllFeaturesHandler : IRequestHandler<GetAllFeaturesRequest, GetAllFeaturesResponse>
    {
        private readonly IProductFeatureServices _productFeatureServices;

        public GetAllFeaturesHandler(IProductFeatureServices productFeatureServices)
        {
            _productFeatureServices = productFeatureServices;
        }
        public async Task<GetAllFeaturesResponse> Handle(GetAllFeaturesRequest request, CancellationToken cancellationToken)
        {
            var (datas, count) = await _productFeatureServices.GetAllFeatures(request.Name, request.Page, request.Size);
            return new()
            {
                Datas = datas.ToList(),
                TotalCount = count
            };

        }
    }
}
