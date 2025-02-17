using FarmAd.Application.Consts;
using FarmAd.Application.CustomAttributes;
using FarmAd.Application.Enums;
using FarmAd.Application.Features.Commands.User.AssignRoleToUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmAd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Admin")]
    public class UserAuthenticationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserAuthenticationsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("GetAll")]
        //[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get All Authentications", Menu = AuthorizeDefinationConstants.UserAuthentications)]
        public async Task<IActionResult> GetAll([FromBody] GetAllUserAuthenticationCommandRequest request)
        {
            GetAllUserAuthenticationCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("RemoveAuthentications")]
        //[AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Remove Authentications", Menu = AuthorizeDefinationConstants.UserAuthentications)]
        public async Task<IActionResult> RemoveAuthentications([FromBody] UserAuthenticationRemoveCommandRequest getRolesToEndpointQueryRequest)
        {
            UserAuthenticationRemoveCommandResponse response = await _mediator.Send(getRolesToEndpointQueryRequest);
            return Ok(response);
        }
    }
}
