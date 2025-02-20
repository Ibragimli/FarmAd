using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.Area;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.SubCategory.CreateSubCategory
{
    public class CreateSubCategoryCommandHandler : IRequestHandler<CreateSubCategoryCommandRequest, CreateSubCategoryCommandResponse>
    {
        private readonly IAdminSubCategoryServices _SubCategoryService;

        public CreateSubCategoryCommandHandler(IAdminSubCategoryServices SubCategoryService)
        {
            _SubCategoryService = SubCategoryService;
        }
        public async Task<CreateSubCategoryCommandResponse> Handle(CreateSubCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            await _SubCategoryService.SubCategoryCreate(request.SubCategoryCreateDto);
            return new()
            {
                Succeeded = true
            };
        }
    }
}
