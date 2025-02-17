using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Queries.Category.GetCategoryById
{
    public class GetCategoryByIdQueryRequest:IRequest<GetCategoryByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
