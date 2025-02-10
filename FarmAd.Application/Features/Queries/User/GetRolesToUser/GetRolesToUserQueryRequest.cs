using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Queries.User.GetRolesToUser
{
    public class GetRolesToUserQueryRequest:IRequest<GetRolesToUserQueryResponse>
    {
        public string UserId { get; set; }
    }
}
