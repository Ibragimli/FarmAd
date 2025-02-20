using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.SubCategory.DeleteSubCategory
{
    public class DeleteSubCategoryCommandRequest : IRequest<DeleteSubCategoryCommandResponse>
    {
        public int Id { get; set; }
    }
}
