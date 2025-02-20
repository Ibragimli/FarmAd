using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.Area;
using FarmAd.Application.Features.Queries.City.GetSubCategories;
using FarmAd.Application.Repositories.SubCategory;
using FarmAd.Domain.Entities.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Queries.SubCategory.GetSubCategories
{
    public class GetSubCategoriesQueriesHandler : IRequestHandler<GetSubCategoriesQueriesRequest, GetSubCategoriesQueriesResponse>
    {
        private readonly IAdminSubCategoryServices _adminSubCategoryServices;

        public GetSubCategoriesQueriesHandler(IAdminSubCategoryServices adminSubCategoryServices)
        {
            _adminSubCategoryServices = adminSubCategoryServices;
        }
        public async Task<GetSubCategoriesQueriesResponse> Handle(GetSubCategoriesQueriesRequest request, CancellationToken cancellationToken)
        {
            var (datas, count) = _adminSubCategoryServices.GetSubCategorys(request.SubCategoryName,request.CategoryName,request.Page, request.Size);
            return new()
            {
                Datas = datas,
                TotalCount = count
            };

        }
    }
}
