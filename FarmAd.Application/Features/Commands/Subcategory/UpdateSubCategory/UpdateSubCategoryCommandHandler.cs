using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.Area;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.SubCategory.UpdateSubCategory
{
    public class UpdateSubCategoryCommandHandler : IRequestHandler<UpdateSubCategoryCommandRequest, UpdateSubCategoryCommandResponse>
    {
        private readonly IAdminSubCategoryServices _SubCategoryService;

        public UpdateSubCategoryCommandHandler(IAdminSubCategoryServices SubCategoryService)
        {
            _SubCategoryService = SubCategoryService;
        }
        public async Task<UpdateSubCategoryCommandResponse> Handle(UpdateSubCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            await _SubCategoryService.SubCategoryUpdate(request.SubCategoryUpdateDto);
            return new()
            {
                Succeeded = true
            };
        }
    }
}
