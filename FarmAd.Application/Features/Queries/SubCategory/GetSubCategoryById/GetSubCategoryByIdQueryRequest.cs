using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Queries.SubCategory.GetSubCategoryById
{
    public class GetSubCategoryByIdQueryRequest:IRequest<GetSubCategoryByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
