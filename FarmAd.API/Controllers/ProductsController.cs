using FarmAd.Application.Consts;
using FarmAd.Application.CustomAttributes;
using FarmAd.Application.Enums;
using FarmAd.Application.Features.Commands.Product.ProductAddWishlistCommand;
using FarmAd.Application.Features.Commands.Product.ProductAuthenticationCommand;
using FarmAd.Application.Features.Commands.Product.ProductCreateCommand;
using FarmAd.Application.Features.Commands.Product.ProductDeleteCommand;
using FarmAd.Application.Features.Commands.Product.ProductDeleteWishlistCommand;
using FarmAd.Application.Features.Commands.Product.ProductEditCommand;
using FarmAd.Application.Features.Queries.Product.GetAllProducts;
using FarmAd.Application.Features.Queries.Product.ProductDetailQueries;
using FarmAd.Application.Features.Queries.Role.GetRoles;
using FarmAd.Persistence.Services.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("ProductDetail/{Id}")]
        public async Task<IActionResult> ProductDetail([FromRoute] ProductDetailQueriesRequest request)
        {
            ProductDetailQueriesResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreateCommandRequest request)
        {
            ProductCreateCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("ProductAddWishlist")]
        public async Task<IActionResult> ProductAddWishlist([FromBody] ProductAddWishlistCommandRequest request)
        {
            ProductAddWishlistCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("ProductDeleteWishlist")]
        public async Task<IActionResult> ProductDeleteWishlist([FromBody] ProductDeleteWishlistCommandRequest request)
        {
            ProductDeleteWishlistCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("EditProduct")]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> EditProduct([FromForm] ProductEditCommandRequest request)
        {
            ProductEditCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("ProductAuthentication")]
        public async Task<IActionResult> ProductAuthentication([FromForm] ProductAuthenticationCommandRequest request)
        {
            ProductAuthenticationCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("DeleteProduct/{Id}")]
        //[Authorize(AuthenticationSchemes = "Admin")]
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
