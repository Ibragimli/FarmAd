using FarmAd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.User.AssignRoleToUser
{
    public class GetAllUserAuthenticationCommandResponse
    {
        public List<UserAuthentication> UserAuthentications { get; set; }
    }

}
