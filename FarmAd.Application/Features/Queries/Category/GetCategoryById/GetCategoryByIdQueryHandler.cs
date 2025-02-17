using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.Area;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Queries.Category.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQueryRequest, GetCategoryByIdQueryResponse>
    {
        private readonly IAdminCategoryServices _CategoryService;

        public GetCategoryByIdQueryHandler(IAdminCategoryServices CategoryService)
        {
            _CategoryService = CategoryService;
        }
        public async Task<GetCategoryByIdQueryResponse> Handle(GetCategoryByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _CategoryService.GetCategory(request.Id);
            return new()
            {
                Id = data.Id,
                Name = data.Name,
                Image = data.Image,
                Path = data.ImagePath
            };
        }
    }
}
