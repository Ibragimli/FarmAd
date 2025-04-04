using FarmAd.Application.Features.Commands.City.CreateCity;
using FarmAd.Application.Features.Commands.City.DeleteCity;
using FarmAd.Application.Features.Commands.City.UpdateCity;
using FarmAd.Application.Features.Commands.ServiceDuration.CreateServiceDuration;
using FarmAd.Application.Features.Commands.ServiceDuration.UpdateServiceDuration;
using FarmAd.Application.Features.Queries.City.GetCities;
using FarmAd.Application.Features.Queries.City.GetCityById;
using FarmAd.Application.Features.Queries.ServiceDuration.GetServiceDurationById;
using FarmAd.Application.Features.Queries.ServiceDuration.GetServiceDurations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmAd.API.Area.Manage.Controllers
{
    [Area("Manage")]
    [Route("api/manage/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Admin")]

    public class ServiceDurationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServiceDurationsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetServiceDurations")]
        //[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Cities", Menu = AuthorizeDefinationConstants.Cities)]
        public async Task<IActionResult> GetServiceDurations([FromQuery] GetServiceDurationsQueriesRequest request)
        {
            GetServiceDurationsQueriesResponse response = await _mediator.Send(request);
            return Ok(response);
        }


        [HttpGet("GetServiceDuration/{Id}")]
        //[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get City By Id", Menu = AuthorizeDefinationConstants.Cities)]
        public async Task<IActionResult> GetServiceDuration([FromRoute] GetServiceDurationByIdQueryRequest request)
        {
            GetServiceDurationByIdQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("CreateServiceDuration")]
        //[AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Create City", Menu = AuthorizeDefinationConstants.Cities)]
        public async Task<IActionResult> CreateServiceDuration([FromBody] CreateServiceDurationCommandRequest request)
        {
            CreateServiceDurationCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("UpdateServiceDuration")]
        //[AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Update City", Menu = AuthorizeDefinationConstants.Cities)]
        public async Task<IActionResult> UpdateServiceDuration([FromBody] UpdateServiceDurationCommandRequest request)
        {
            UpdateServiceDurationCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        //[HttpDelete("DeleteCity/{Id}")]
        ////[AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Delete City", Menu = AuthorizeDefinationConstants.Cities)]
        //public async Task<IActionResult> DeleteCity([FromRoute] DeleteCityCommandRequest request)
        //{
        //    DeleteCityCommandResponse response = await _mediator.Send(request);
        //    return Ok(response);
        //}
    }
}
