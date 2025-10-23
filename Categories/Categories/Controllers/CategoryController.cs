using Categories.Models;
using Categories.Services;
using Microsoft.AspNetCore.Mvc;

namespace Categories.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        /// <summary>
        /// Gets category information with inherited keywords (Problem 1)
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <returns>Formatted category information with inherited keywords</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<CategoryInfoResponse>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 404)]
        public ActionResult<ApiResponse<CategoryInfoResponse>> GetCategoryInfo(int id)
        {
            try
            {
                var result = _categoryService.GetCategoryInfoDetailed(id);
                return Ok(new ApiResponse<CategoryInfoResponse>
                {
                    Success = true,
                    Data = result
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// Gets category information as formatted string (Problem 1 - Original Format)
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <returns>Comma-delimited string of category properties</returns>
        [HttpGet("{id}/formatted")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 404)]
        public ActionResult<ApiResponse<string>> GetCategoryInfoFormatted(int id)
        {
            try
            {
                var result = _categoryService.GetCategoryInfo(id);
                return Ok(new ApiResponse<string>
                {
                    Success = true,
                    Data = result
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// Gets all category IDs at a specific hierarchy level (Problem 2)
        /// </summary>
        /// <param name="level">Hierarchy level (1 = root categories)</param>
        /// <returns>List of category IDs at the specified level</returns>
        [HttpGet("level/{level}")]
        [ProducesResponseType(typeof(ApiResponse<CategoryLevelResponse>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        public ActionResult<ApiResponse<CategoryLevelResponse>> GetCategoriesByLevel(int level)
        {
            try
            {
                var categoryIds = _categoryService.GetCategoriesByLevel(level);
                var response = new CategoryLevelResponse
                {
                    Level = level,
                    CategoryIds = categoryIds,
                    Count = categoryIds.Count
                };

                return Ok(new ApiResponse<CategoryLevelResponse>
                {
                    Success = true,
                    Data = response
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Error = ex.Message
                });
            }
        }
    }
}
