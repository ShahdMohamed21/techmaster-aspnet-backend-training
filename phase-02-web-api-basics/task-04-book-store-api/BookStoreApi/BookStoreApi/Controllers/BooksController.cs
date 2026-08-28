using BookStoreApi.DTOs.Books;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService bookService;
        public BooksController(IBookService _bookservice)
        {
            bookService = _bookservice;
            
        }
        [HttpPost]
        public IActionResult CreateBook(CreateBookRequest request)
        {
            try
            {
                var book = bookService.CreateBook(request);
                return StatusCode(201, book);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books= bookService.GetAllBooks();
            return Ok(books);
        }
        [HttpGet("{id}")]
        public IActionResult GetBookById(int id) {
            var book= bookService.GetBookById(id);
            if(book==null)
            {
                return NotFound("Book Does Not Exist");
            }
            return Ok(book);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,UpdateBookRequest request)
        {
            try
            {
                var UpdatedBook = bookService.UpdateBook(id, request);
                return Ok(UpdatedBook);

            }
            catch(ArgumentNullException ex)
            {
                return NotFound(ex.Message);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var deleted = bookService.DeleteBook(id);

            if (!deleted)
            {
                return NotFound("Book Does Not Exist");
            }

            return Ok("Book Deleted");
        }




    }
}
