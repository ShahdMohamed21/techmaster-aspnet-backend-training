using BookStoreApi.DTOs.Categories;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = _categoryService.GetAllCategories();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _categoryService.GetCategoryById(id);

            if (category == null)
            {
                return NotFound("Category Does Not Exist");
            }

            return Ok(category);
        }

        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryRequest request)
        {
            try
            {
                var category = _categoryService.CreateCategory(request);

                return StatusCode(StatusCodes.Status201Created,category);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id,UpdateCategoryRequest request)
        {
            try
            {
                var category = _categoryService.UpdateCategory(id, request);

                if (category == null)
                {
                    return NotFound("Category Does Not Exist");
                }

                return Ok(category);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _categoryService.DeleteCategory(id);

            if (category == false)
            {
                return NotFound("Category Does Not Exist");
            }

            return Ok("Category Deleted");
        }
    }
}