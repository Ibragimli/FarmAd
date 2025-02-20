using FarmAd.Application.Consts;
using FarmAd.Application.CustomAttributes;
using FarmAd.Application.Enums;
using FarmAd.Application.Features.Commands.City.CreateCity;
using FarmAd.Application.Features.Commands.City.DeleteCity;
using FarmAd.Application.Features.Commands.City.UpdateCity;
using FarmAd.Application.Features.Queries.City.GetCities;
using FarmAd.Application.Features.Queries.City.GetCityById;
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

    public class CitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetCities")]
        //[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Cities", Menu = AuthorizeDefinationConstants.Cities)]
        public async Task<IActionResult> GetCities([FromQuery] GetCitiesQueriesRequest request)
        {
            GetCitiesQueriesResponse response = await _mediator.Send(request);
            return Ok(response);
        }


        [HttpGet("GetCity/{Id}")]
        //[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get City By Id", Menu = AuthorizeDefinationConstants.Cities)]
        public async Task<IActionResult> GetCity([FromRoute] GetCityByIdQueryRequest request)
        {
            GetCityByIdQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("CreateCity")]
        //[AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Create City", Menu = AuthorizeDefinationConstants.Cities)]
        public async Task<IActionResult> CreateCity([FromBody] CreateCityCommandRequest request)
        {
            CreateCityCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("UpdateCity")]
        //[AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Update City", Menu = AuthorizeDefinationConstants.Cities)]
        public async Task<IActionResult> UpdateCity([FromBody] UpdateCityCommandRequest request)
        {
            UpdateCityCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("DeleteCity/{Id}")]
        //[AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Delete City", Menu = AuthorizeDefinationConstants.Cities)]
        public async Task<IActionResult> DeleteCity([FromRoute] DeleteCityCommandRequest request)
        {
            DeleteCityCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
