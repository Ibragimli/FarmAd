using FarmAd.Application.Consts;
using FarmAd.Application.CustomAttributes;
using FarmAd.Application.Enums;
using FarmAd.Application.Features.Commands.SubCategory.CreateSubCategory;
using FarmAd.Application.Features.Commands.SubCategory.DeleteSubCategory;
using FarmAd.Application.Features.Commands.SubCategory.UpdateSubCategory;
using FarmAd.Application.Features.Queries.City.GetSubCategories;
using FarmAd.Application.Features.Queries.SubCategory.GetSubCategories;
using FarmAd.Application.Features.Queries.SubCategory.GetSubCategoryById;
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

    public class SubCategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubCategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetSubCategories")]
        //[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get SubCategories", Menu = AuthorizeDefinationConstants.SubCategories)]
        public async Task<IActionResult> GetSubCategories([FromBody] GetSubCategoriesQueriesRequest request)
        {
            GetSubCategoriesQueriesResponse response = await _mediator.Send(request);
            return Ok(response);
        }


        [HttpGet("GetSubCategory/{Id}")]
        //[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get SubCategory By Id", Menu = AuthorizeDefinationConstants.SubCategories)]
        public async Task<IActionResult> GetSubCategory([FromRoute] GetSubCategoryByIdQueryRequest request)
        {
            GetSubCategoryByIdQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("CreateSubCategory")]
        //[AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Create SubCategory", Menu = AuthorizeDefinationConstants.SubCategories)]
        public async Task<IActionResult> CreateSubCategory([FromBody] CreateSubCategoryCommandRequest request)
        {
            CreateSubCategoryCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("UpdateSubCategory")]
        //[AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Update SubCategory", Menu = AuthorizeDefinationConstants.SubCategories)]
        public async Task<IActionResult> UpdateSubCategory([FromBody] UpdateSubCategoryCommandRequest request)
        {
            UpdateSubCategoryCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("DeleteSubCategory/{Id}")]
        //[AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Delete SubCategory", Menu = AuthorizeDefinationConstants.SubCategories)]
        public async Task<IActionResult> DeleteSubCategory([FromRoute] DeleteSubCategoryCommandRequest request)
        {
            DeleteSubCategoryCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
