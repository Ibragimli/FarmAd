using FarmAd.Application.Features.Queries;
using FarmAd.Application.Features.Queries.ProductFeature.GetAllFeatures;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FarmAd.API.Area.Manage.Controllers
{
    [Area("Manage")]
    [Route("api/manage/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Admin")]

    public class FeaturesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FeaturesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAllFeatures")]
        //[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "GetFeatures", Menu = AuthorizeDefinationConstants.Features)]
        public async Task<IActionResult> GetAllFeatures([FromQuery] GetAllFeaturesRequest request)
        {
            GetAllFeaturesResponse response = await _mediator.Send(request);
            return Ok(response);
        }

    }
}
