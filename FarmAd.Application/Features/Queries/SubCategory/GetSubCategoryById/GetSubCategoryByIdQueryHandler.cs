using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.Area;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Queries.SubCategory.GetSubCategoryById
{
    public class GetSubCategoryByIdQueryHandler : IRequestHandler<GetSubCategoryByIdQueryRequest, GetSubCategoryByIdQueryResponse>
    {
        private readonly IAdminSubCategoryServices _SubCategoryService;

        public GetSubCategoryByIdQueryHandler(IAdminSubCategoryServices SubCategoryService)
        {
            _SubCategoryService = SubCategoryService;
        }
        public async Task<GetSubCategoryByIdQueryResponse> Handle(GetSubCategoryByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _SubCategoryService.GetSubCategory(request.Id);
            return new()
            {
                Id = data.Id,
                Name = data.Name,
                Category = data.Category.Name
            };
        }
    }
}
