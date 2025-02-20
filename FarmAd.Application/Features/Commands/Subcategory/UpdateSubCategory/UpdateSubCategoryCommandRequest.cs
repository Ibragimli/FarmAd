using FarmAd.Application.DTOs.Area;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.SubCategory.UpdateSubCategory
{
    public class UpdateSubCategoryCommandRequest : IRequest<UpdateSubCategoryCommandResponse>
    {
        public SubCategoryUpdateDto SubCategoryUpdateDto { get; set; }
    }
}
