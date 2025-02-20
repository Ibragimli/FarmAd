using FarmAd.Application.Consts;
using FarmAd.Application.CustomAttributes;
using FarmAd.Application.Enums;
using FarmAd.Application.Features.Commands.Setting.UpdateSetting;
using FarmAd.Application.Features.Queries.Role.GetRoles;
using FarmAd.Application.Features.Queries.Settings.GetSettings;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FarmAd.API.Area.Manage.Controllers
{
    [Area("Manage")]
    [Route("api/manage/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Admin")]

    public class SettingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SettingsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetSettings")]
        //[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Settings", Menu = AuthorizeDefinationConstants.Settings)]
        public async Task<IActionResult> GetSettings([FromQuery] GetSettingsQueriesRequest request)
        {
            GetSettingsQueriesResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("UpdateSetting")]
        //[AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Update Setting", Menu = AuthorizeDefinationConstants.Settings)]
        public async Task<IActionResult> UpdateSetting([FromForm] UpdateSettingCommandRequest request)
        {
            UpdateSettingCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

    }
}
