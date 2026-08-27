using Microsoft.AspNetCore.Mvc;
using ProductsCategoriesApi.DTOs;
using ProductsCategoriesApi.Models;
using ProductsCategoriesApi.Services;

namespace ProductsCategoriesApi.Controllers
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
        public IActionResult GetAllCategories(){
            var categories = _categoryService.GetAllCategories();
            return Ok(categories);
        }
        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryRequest request) {
            var Category=_categoryService.AddCategory(request);

            return Created("", Category);
        }
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id) { 
            var category= _categoryService.GetCateogoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id,UpdateCategoryRequest request)
        {
            var category = _categoryService.UpdateCategory(id, request);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _categoryService.DeleteCategory(id);
            if (category == false)
            {
                return NotFound();
            }
            return Ok("Category Deleted");
        }


    }
}
