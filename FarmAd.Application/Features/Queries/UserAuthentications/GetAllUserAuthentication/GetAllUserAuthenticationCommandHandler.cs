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

    public class GetAllUserAuthenticationCommandHandler : IRequestHandler<GetAllUserAuthenticationCommandRequest, GetAllUserAuthenticationCommandResponse>
    {
        private readonly IAuthService _authService;

        public GetAllUserAuthenticationCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<GetAllUserAuthenticationCommandResponse> Handle(GetAllUserAuthenticationCommandRequest request, CancellationToken cancellationToken)
        {
            var userAuthentications = await _authService.GetAllUserAuthentication(request.Page, request.Size);
            return new()
            {
                UserAuthentications = userAuthentications
            };
        }
    }
}
