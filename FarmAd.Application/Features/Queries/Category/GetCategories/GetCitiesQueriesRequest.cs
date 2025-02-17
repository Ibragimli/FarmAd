using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Queries.Role.GetRoles
{
    public class GetCategoriesQueriesRequest : IRequest<GetCategoriesQueriesResponse>
    {
        public string? Name { get; set; }
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
