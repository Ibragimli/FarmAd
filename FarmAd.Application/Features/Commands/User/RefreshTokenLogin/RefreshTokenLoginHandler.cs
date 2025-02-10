using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.DTOs;
using MediatR;

namespace FarmAd.Application.Features.Commands.User.RefreshTokenLogin
{
    public class RefreshTokenLoginHandler : IRequestHandler<RefreshTokenLoginRequest, RefreshTokenLoginResponse>
    {
        private readonly IAuthService _authService;

        public RefreshTokenLoginHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<RefreshTokenLoginResponse> Handle(RefreshTokenLoginRequest request, CancellationToken cancellationToken)
        {
            Token token = await _authService.RefreshTokenLoginAsync(request.RefreshToken);

            return new()
            {
                Token = token
            };
        }
    }
}
