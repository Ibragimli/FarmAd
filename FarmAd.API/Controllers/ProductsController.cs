using FarmAd.Application.Consts;
using FarmAd.Application.CustomAttributes;
using FarmAd.Application.Enums;
using FarmAd.Application.Features.Commands.Product.ProductCreateCommand;
using FarmAd.Application.Features.Commands.Product.ProductDeleteCommand;
using FarmAd.Application.Features.Queries.Product.GetAllProducts;
using FarmAd.Application.Features.Queries.Role.GetRoles;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FarmAd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts([FromQuery] GetAllProductsRequest request)
        {
            GetAllProductsResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreateCommandRequest request)
        {
            ProductCreateCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("DeleteProduct/{Id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] ProductDeleteCommandRequest request)
        {
            ProductDeleteCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("CreateImage")]
        public async Task<IActionResult> CreateImage([FromForm] ProductCreateCommandRequest request)
        {
            ProductCreateCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("CreateImages")]
        //[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Roles", Menu = AuthorizeDefinationConstants.Roles)]
        public async Task<IActionResult> CreateImages([FromForm] ProductCreateCommandRequest request)
        {
            ProductCreateCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
