using FarmAd.Application.Features.Commands.Product.Area.ProductActiveCommand;
using FarmAd.Application.Features.Commands.Product.Area.ProductDisabledCommand;
using FarmAd.Application.Features.Commands.Product.Area.ProductEditCommand;
using FarmAd.Application.Features.Queries.Product.Area.GetAllProducts;
using FarmAd.Application.Features.Queries.Product.Area.ProductDetailQueries;
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

    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAllProducts")]
        //[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "GetFeatures", Menu = AuthorizeDefinationConstants.Features)]
        public async Task<IActionResult> GetAllProducts([FromBody] GetAllProductsRequest request)
        {
            GetAllProductsResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("ProductDetail/{Id}")]
        //[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "GetFeatures", Menu = AuthorizeDefinationConstants.Features)]
        public async Task<IActionResult> ProductDetail([FromRoute] ProductDetailQueriesRequest request)
        {
            ProductDetailQueriesResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut("EditProduct")]
        //[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "GetFeatures", Menu = AuthorizeDefinationConstants.Features)]
        public async Task<IActionResult> EditProduct([FromBody] ProductEditCommandRequest request)
        {
            ProductEditCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut("ProductActive/{Id}")]
        //[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "GetFeatures", Menu = AuthorizeDefinationConstants.Features)]
        public async Task<IActionResult> ProductActive([FromRoute] ProductActiveCommandRequest request)
        {
            ProductActiveCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut("ProductDisabled/{Id}")]
        //[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "GetFeatures", Menu = AuthorizeDefinationConstants.Features)]
        public async Task<IActionResult> ProductDisabled([FromRoute] ProductDisabledCommandRequest request)
        {
            ProductDisabledCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

    }
}
