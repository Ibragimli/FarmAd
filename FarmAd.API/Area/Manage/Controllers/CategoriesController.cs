using FarmAd.Application.Consts;
using FarmAd.Application.CustomAttributes;
using FarmAd.Application.Enums;
using FarmAd.Application.Features.Commands.Category.CreateCategory;
using FarmAd.Application.Features.Commands.Category.DeleteCategory;
using FarmAd.Application.Features.Commands.Category.UpdateCategory;
using FarmAd.Application.Features.Queries.Category.GetCategoryById;
using FarmAd.Application.Features.Queries.Role.GetRoles;
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

    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetCategories")]
        //[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Categories", Menu = AuthorizeDefinationConstants.Categories)]
        public async Task<IActionResult> GetCategories([FromQuery] GetCategoriesQueriesRequest request)
        {
            GetCategoriesQueriesResponse response = await _mediator.Send(request);
            return Ok(response);
        }


        [HttpGet("GetCategory/{Id}")]
        //[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Category By Id", Menu = AuthorizeDefinationConstants.Categories)]
        public async Task<IActionResult> GetCategory([FromRoute] GetCategoryByIdQueryRequest request)
        {
            GetCategoryByIdQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("CreateCategory")]
        //[AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Create Category", Menu = AuthorizeDefinationConstants.Categories)]
        public async Task<IActionResult> CreateCategory([FromForm] CreateCategoryCommandRequest request)
        {
            CreateCategoryCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("UpdateCategory")]
        //[AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Update Category", Menu = AuthorizeDefinationConstants.Categories)]
        public async Task<IActionResult> UpdateCategory([FromForm] UpdateCategoryCommandRequest request)
        {
            UpdateCategoryCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("DeleteCategory/{Id}")]
        //[AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Delete Category", Menu = AuthorizeDefinationConstants.Categories)]
        public async Task<IActionResult> DeleteCategory([FromRoute] DeleteCategoryCommandRequest request)
        {
            DeleteCategoryCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
