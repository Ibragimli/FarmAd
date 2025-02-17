using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.User.AssignRoleToUser
{
    public class GetAllUserAuthenticationCommandRequest : IRequest<GetAllUserAuthenticationCommandResponse>
    {
        public int Page { get; set; }
        public int Size { get; set; }
    }
}
