using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
