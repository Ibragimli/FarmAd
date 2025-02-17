using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.User.AssignRoleToUser
{
    public class UserAuthenticationRemoveCommandRequest : IRequest<UserAuthenticationRemoveCommandResponse>
    {
        public int Id { get; set; }
    }
}
