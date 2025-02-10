using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.User.AssignRoleToUser
{
    public class AssignRoleToUserRequest : IRequest<AssignRoleToUserResponse>
    {
        public string UserId { get; set; }
        public string[] Roles { get; set; }
    }
}
