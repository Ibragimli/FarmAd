using FarmAd.Application.Consts;
using FarmAd.Application.CustomAttributes;
using FarmAd.Application.Enums;
using FarmAd.Application.Features.Commands.ContactUs.ContactUsCreateCommand;
using FarmAd.Application.Features.Commands.ContactUs.ContactUsDeleteCommand;
using FarmAd.Application.Features.Commands.ContactUs.ContactUsRespondCommand;
using FarmAd.Application.Features.Commands.Role.DeleteRole;
using FarmAd.Application.Features.Queries.ContactUs.GetContactUsQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FarmAd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactUsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateContactUs")]
        public async Task<IActionResult> CreateContactUs([FromBody] ContactUsCreateCommandRequest request)
        {
            ContactUsCreateCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        //[Authorize(AuthenticationSchemes = "Admin")]
        [HttpDelete("DeleteContactUs/{Id}")]
        public async Task<IActionResult> DeleteContactUs([FromRoute] ContactUsDeleteCommandRequest request)
        {
            ContactUsDeleteCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        //[Authorize(AuthenticationSchemes = "Admin")]
        [HttpPost("RespondContactUs")]
        public async Task<IActionResult> RespondContactUs([FromBody] ContactUsRespondCommandRequest request)
        {
            ContactUsRespondCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("GetAllContactUs")]
        public async Task<IActionResult> GetAllContactUs([FromBody] GetContactUsQueriesRequest request)
        {
            GetContactUsQueriesResponse response = await _mediator.Send(request);
            return Ok(response);
        }


    }
}
