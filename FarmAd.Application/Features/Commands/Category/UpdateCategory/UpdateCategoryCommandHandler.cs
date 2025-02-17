using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.Area;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.Category.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest, UpdateCategoryCommandResponse>
    {
        private readonly IAdminCategoryServices _CategoryService;

        public UpdateCategoryCommandHandler(IAdminCategoryServices CategoryService)
        {
            _CategoryService = CategoryService;
        }
        public async Task<UpdateCategoryCommandResponse> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            await _CategoryService.CategoryEdit(request.CategoryUpdateDto);
            return new()
            {
                Succeeded = true
            };
        }
    }
}
