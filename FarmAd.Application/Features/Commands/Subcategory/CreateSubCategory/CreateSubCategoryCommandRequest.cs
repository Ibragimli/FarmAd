using FarmAd.Application.DTOs.Area;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.SubCategory.CreateSubCategory
{
    public class CreateSubCategoryCommandRequest : IRequest<CreateSubCategoryCommandResponse>
    {
        public SubCategoryCreateDto SubCategoryCreateDto { get; set; }

    }
}
