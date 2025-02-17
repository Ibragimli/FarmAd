using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.Area;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.Category.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommandRequest, DeleteCategoryCommandResponse>
    {
        private readonly IAdminCategoryServices _CategoryService;

        public DeleteCategoryCommandHandler(IAdminCategoryServices CategoryService)
        {
            _CategoryService = CategoryService;
        }
        public async Task<DeleteCategoryCommandResponse> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            await _CategoryService.CategoryDelete(request.Id);
            return new()
            {
                Succeeded = true
            };
        }
    }
}
