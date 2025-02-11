using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Tokens;
using FarmAd.Application.DTOs;
using FarmAd.Application.DTOs.User;
using FarmAd.Application.Exceptions;
using FarmAd.Domain.Entities.Identity;
using FarmAd.Infrastructure.Service.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace FarmAd.Application.Features.Queries.User.GetAllUsers
{
    public class GetAllUsersCommandHandler : IRequestHandler<GetAllUsersCommandRequest, GetAllUsersCommandResponse>
    {
        private readonly IAuthService _authService;

        public GetAllUsersCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<GetAllUsersCommandResponse> Handle(GetAllUsersCommandRequest request, CancellationToken cancellationToken)
        {
            List<AllUserDto> dtos = await _authService.GetAllUser();
            return new()
            {
                AllUserDtos = dtos,
            };
        }
    }
}
