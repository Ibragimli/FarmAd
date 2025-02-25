using FarmAd.Infrastructure.Service.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Commands.User.SignOutUser
{
    public class SignOutUserCommandHandler : IRequestHandler<SignOutUserCommandRequest, SignOutUserCommandResponse>
    {
        private readonly IUserService _userService;

        public SignOutUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<SignOutUserCommandResponse> Handle(SignOutUserCommandRequest request, CancellationToken cancellationToken)
        {
            string username = _userService.IdentityUser();
            await _userService.SignOutUser();
            return new()
            {
                Username =username,
                Successed = true
            };
        }
    }
}
