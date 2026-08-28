using BookStoreApi.DTOs.Authors;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService authorService;
        public AuthorsController(IAuthorService _authorService)
        {
            authorService = _authorService;
        }
        [HttpPost]
        public IActionResult CraeteAuthor(CreateAuthorRequest request)
        {
            try
            {
                var author = authorService.CreateAuthor(request);
                return Created("", author);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult GetAllAuthors() {
            var authors = authorService.GetAllAuthors();
            return Ok(authors);
        }
        [HttpGet("{id}")]
        public IActionResult GetAuthorById(int id) {
            var author=authorService.GetAuthorById(id);
            if(author == null)
            {
                return NotFound("Author Does Not Exist");
            }
            return Ok(author);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id ,UpdateAuthorRequest request) { 
            var author=authorService.UpdateAuthor(id, request);
            if (author == null)
            {
                return NotFound("Author Does Not Exist");
            }
            return Ok(author);

        }


    }
}
