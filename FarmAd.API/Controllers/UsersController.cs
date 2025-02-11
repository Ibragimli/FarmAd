using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.DTOs;
using FarmAd.Application.Features.Commands.User.LoginAuthentication;
using FarmAd.Application.Features.Commands.User.LoginUser;
using FarmAd.Application.Features.Queries.User.GetAllUsers;
using FarmAd.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FarmAd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthService _authService;

        public UsersController(IMediator mediator, IAuthService authService)
        {
            _mediator = mediator;
            _authService = authService;
        }
        // POST api/user/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto model)
        {
            var result = await _authService.CreateUserPostman(model);

            if (result)
                return Ok(new { Message = "User created successfully!" });

            return BadRequest("sad");
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersCommandRequest request)
        {
            GetAllUsersCommandResponse response = new();
            try
            {
                response = await _mediator.Send(request);

            }
            catch (Exception ms)
            {
                return Ok(ms.Message);
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest request)
        {
            LoginUserCommandResponse response = new();
            try
            {
                response = await _mediator.Send(request);

            }
            catch (Exception ms)
            {
                return Ok(ms.Message);
            }
            return Ok(response);
        }
        [HttpPost("LoginAuthentication")]
        public async Task<IActionResult> LoginAuthentication([FromBody] LoginAuthenticationCommandRequest request)
        {
            LoginAuthenticationCommandResponse response = new();
            try
            {
                response = await _mediator.Send(request);
            }
            catch (Exception ms)
            {
                return Ok(ms.Message);
            }
            return Ok(response);
        }
    }
}
