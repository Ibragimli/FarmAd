using FarmAd.Application.Consts;
using FarmAd.Application.CustomAttributes;
using FarmAd.Application.Enums;
using FarmAd.Application.Features.Commands.Role.CreateRole;
using FarmAd.Application.Features.Commands.Role.DeleteRole;
using FarmAd.Application.Features.Commands.Role.UpdateRole;
using FarmAd.Application.Features.Queries.Role.GetRoleById;
using FarmAd.Application.Features.Queries.Role.GetRoles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FarmAd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Admin")]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetRoles")]
        //[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Roles", Menu = AuthorizeDefinationConstants.Roles)]
        public async Task<IActionResult> GetRoles([FromQuery] GetRolesQueriesRequest getRolesQueryRequest)
        {
            GetRolesQueriesResponse response = await _mediator.Send(getRolesQueryRequest);
            return Ok(response);
        }
   
        [HttpGet("CheckAuth")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Check value", Menu = AuthorizeDefinationConstants.Roles)]
        public IActionResult CheckAuth()
        {
            var username = User.Identity?.Name;
            var claims = User.Claims.Select(c => new { c.Type, c.Value });

            return Ok(new { Username = username, Claims = claims });
        }
        
        [HttpGet("GetRole/{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Role By Id", Menu = AuthorizeDefinationConstants.Roles)]
        public async Task<IActionResult> GetRole([FromRoute] GetRoleByIdQueryRequest getRoleByIdQueryRequest)
        {
            GetRoleByIdQueryResponse response = await _mediator.Send(getRoleByIdQueryRequest);
            return Ok(response);
        }
        
        [HttpPost("CreateRole")]
        //[AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Create Role", Menu = AuthorizeDefinationConstants.Roles)]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommandRequest createRoleCommandRequest)
        {
            CreateRoleCommandResponse response = await _mediator.Send(createRoleCommandRequest);
            return Ok(response);
        }
        
        [HttpPut("UpdateRole")]
        [AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Update Role", Menu = AuthorizeDefinationConstants.Roles)]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleCommandRequest updateRoleCommandRequest)
        {
            UpdateRoleCommandResponse response = await _mediator.Send(updateRoleCommandRequest);
            return Ok(response);
        }
        
        [HttpDelete("DeleteRole/{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Delete Role", Menu = AuthorizeDefinationConstants.Roles)]
        public async Task<IActionResult> DeleteRole([FromRoute] DeleteRoleCommandRequest deleteRoleCommandRequest)
        {
            DeleteRoleCommandResponse response = await _mediator.Send(deleteRoleCommandRequest);
            return Ok(response);
        }
    }
}
