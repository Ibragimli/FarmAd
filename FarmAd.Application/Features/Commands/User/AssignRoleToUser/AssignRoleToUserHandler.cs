using FarmAd.Application.Abstractions.Services;
using FarmAd.Infrastructure.Service.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.User.AssignRoleToUser
{
    public class AssignRoleToUserHandler : IRequestHandler<AssignRoleToUserRequest, AssignRoleToUserResponse>
    {
        private readonly IUserService _userService;

        public AssignRoleToUserHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<AssignRoleToUserResponse> Handle(AssignRoleToUserRequest request, CancellationToken cancellationToken)
        {
            //await _userService.AssingRoleToUserAsync(request.UserId,request.Roles);
            return new();
        }
    }
}
