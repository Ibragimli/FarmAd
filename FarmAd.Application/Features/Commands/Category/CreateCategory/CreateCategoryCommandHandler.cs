using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.Area;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.Category.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest, CreateCategoryCommandResponse>
    {
        private readonly IAdminCategoryServices _CategoryService;

        public CreateCategoryCommandHandler(IAdminCategoryServices CategoryService)
        {
            _CategoryService = CategoryService;
        }
        public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            await _CategoryService.CategoryCreate(request.CategoryCreateDto);
            return new()
            {
                Succeeded = true
            };
        }
    }
}
