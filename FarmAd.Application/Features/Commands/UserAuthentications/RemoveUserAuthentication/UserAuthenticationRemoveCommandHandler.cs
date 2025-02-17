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

    public class UserAuthenticationRemoveCommandHandler : IRequestHandler<UserAuthenticationRemoveCommandRequest, UserAuthenticationRemoveCommandResponse>
    {
        private readonly IAuthService _authService;

        public UserAuthenticationRemoveCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<UserAuthenticationRemoveCommandResponse> Handle(UserAuthenticationRemoveCommandRequest request, CancellationToken cancellationToken)
        {
            await _authService.RareLimitAllDelete();
            return new()
            {
                Succeed = true
            };
        }
    }
}
