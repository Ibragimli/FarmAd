using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.Area;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.SubCategory.DeleteSubCategory
{
    public class DeleteSubCategoryCommandHandler : IRequestHandler<DeleteSubCategoryCommandRequest, DeleteSubCategoryCommandResponse>
    {
        private readonly IAdminSubCategoryServices _SubCategoryService;

        public DeleteSubCategoryCommandHandler(IAdminSubCategoryServices SubCategoryService)
        {
            _SubCategoryService = SubCategoryService;
        }
        public async Task<DeleteSubCategoryCommandResponse> Handle(DeleteSubCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            await _SubCategoryService.SubCategoryDelete(request.Id);
            return new()
            {
                Succeeded = true
            };
        }
    }
}
