using Microsoft.AspNetCore.Mvc;
using ProductsCategoriesApi.DTOs;
using ProductsCategoriesApi.Services;

namespace ProductsCategoriesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;

        }
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }
        [HttpPost]
        public IActionResult CreateProduct(CreateProductRequest request)
        {
            var product = _productService.AddProduct(request);

            return Created("", product);
        }
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product =_productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, UpdateProductRequest request)
        {
            var product = _productService.UpdateProduct(id, request);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _productService.DeleteProduct(id);
            if (product== false)
            {
                return NotFound();
            }
            return Ok("Product Deleted");
        }
        [HttpGet("search")]
        public IActionResult Search(string? name, int? categoryId, decimal? minPrice, decimal? maxPrice, bool? isAvailable, bool? lowStock) { 
            var products= _productService.SearchProducts(name, categoryId, minPrice, maxPrice, isAvailable, lowStock);
            return Ok(products);
        }
        [HttpGet("Reports")]
        public IActionResult GetReport() {
            var report = _productService.GetReports();
            return Ok (report);
        }
    }
}
