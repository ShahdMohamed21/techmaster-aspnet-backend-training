using AutoMapper;
using BookStoreApi.DTOs.Books;
using BookStoreApi.Models;

namespace BookStoreApi.Services
{
    public class BookService : IBookService
    {
        private readonly ICategoryService _categoryService;
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public BookService(ICategoryService categoryService, IAuthorService authorService, IMapper mapper)
        {
            _categoryService = categoryService;
            _authorService = authorService;
            _mapper = mapper;
        }
        private readonly List<Book> books = new List<Book> {

           new Book
           {
               BookId = 1,
               Title = "C# Fundamentals",
               ISBN = "978-0131103627",
               PublishedYear = 2020,
               Price = 450,
               StockQuantity = 10,
               AuthorId = 1,
               CategoryId = 1,
               IsAvailable = true,
               CreatedAt = DateTime.Now
           },
           new Book
           {
               BookId = 2,
               Title = "Clean Code",
               ISBN = "978-0132350884",
               PublishedYear = 2008,
               Price = 600,
               StockQuantity = 5,
               AuthorId = 2,
               CategoryId = 1,
               IsAvailable = true,
               CreatedAt = DateTime.Now
           },
           new Book
           {
               BookId = 3,
               Title = "A Brief History of Time",
               ISBN = "978-0553380163",
               PublishedYear = 1988,
               Price = 350,
               StockQuantity = 0,
               AuthorId = 3,
               CategoryId = 2,
               IsAvailable = false,
               CreatedAt = DateTime.Now
           },
           new Book
           {
               BookId = 4,
               Title = "The History of the Ancient World",
               ISBN = "978-0465063970",
               PublishedYear = 2011,
               Price = 500,
               StockQuantity = 8,
               AuthorId = 1,
               CategoryId = 3,
               IsAvailable = true,
               CreatedAt = DateTime.Now
           } };

  
        private int _nextId = 5;
        public BookResponse CreateBook(CreateBookRequest request)
        {
           if(string.IsNullOrWhiteSpace(request.Title))
            {
                throw new ArgumentException("Title Is Required");
            }
            if (string.IsNullOrWhiteSpace(request.ISBN))
            {
                throw new ArgumentException("ISBN Is Required");
            }
            bool IsISBNExist = books.Any(x => x.ISBN.Equals(request.ISBN, StringComparison.OrdinalIgnoreCase));
            if(IsISBNExist)
            {
                throw new ArgumentException("ISBN Already Exist");

            }
            if(request.Price<=0)
            {
                throw new ArgumentException("Price must be positive");

            }
            if(request.StockQuantity<0)
            {
                throw new ArgumentException("Stock cannot be negative");
            }
            var IsCategoryExist=_categoryService.GetCategoryById(request.CategoryId);
            if(IsCategoryExist==null)
            {
                throw new ArgumentException("Category Does Not Exist");
            }
            if (!IsCategoryExist.IsActive)
            {
                throw new ArgumentException("Cannot use an inactive category");
            }
            var IsAuthorExist = _authorService.GetAuthorById(request.AuthorId);
            if (IsAuthorExist == null)
            {
                throw new ArgumentException("Author Does Not Exist");
            }
            var book=_mapper.Map<Book>(request);
            book.BookId = _nextId++;
            book.CreatedAt = DateTime.UtcNow;
            book.IsAvailable = book.StockQuantity > 0;
            books.Add(book);

            var BookResponse=_mapper.Map<BookResponse>(book);
            return BookResponse;

        }

       public  bool DeleteBook(int id)
        {
            var book=books.FirstOrDefault(x=> x.BookId==id);
            if(book==null)
            {
                return false;
            }
            books.Remove(book);
            return true;
        }

       public List<BookResponse> GetAllBooks()
        {
            var allbooks = _mapper.Map <List<BookResponse>>(books);
            return allbooks;

        }

        public BookResponse? GetBookById(int id)
        {
            var book=books.FirstOrDefault(x=> x.BookId==id);
            if( book==null )
            {
                return null;
            }
            var bookrespone=_mapper.Map<BookResponse>(book);
            return bookrespone;
         }

       public  BookResponse UpdateBook(int id, UpdateBookRequest request)
        {
            var book = books.FirstOrDefault(x => x.BookId == id);
            if (book == null)
            {
                throw new ArgumentNullException("Book Does Not Exist");
            }
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                throw new ArgumentException("Title Is Required");
            }
            if (string.IsNullOrWhiteSpace(request.ISBN))
            {
                throw new ArgumentException("ISBN Is Required");
            }
            bool IsISBNExist = books.Any(x=>x.BookId != id &&  x.ISBN.Equals(request.ISBN, StringComparison.OrdinalIgnoreCase));
            if (IsISBNExist)
            {
                throw new ArgumentException("ISBN Already Exist");

            }
            if (request.Price <= 0)
            {
                throw new ArgumentException("Price must be positive");

            }
            if (request.StockQuantity < 0)
            {
                throw new ArgumentException("Stock cannot be negative");
            }
            var IsCategoryExist = _categoryService.GetCategoryById(request.CategoryId);
            if (IsCategoryExist == null)
            {
                throw new ArgumentException("Category Does Not Exist");
            }
            if (!IsCategoryExist.IsActive)
            {
                throw new ArgumentException("Cannot use an inactive category");
            }
            var IsAuthorExist = _authorService.GetAuthorById(request.AuthorId);
            if (IsAuthorExist == null)
            {
                throw new ArgumentException("Author Does Not Exist");
            }
            _mapper.Map(request, book);
            book.IsAvailable = book.StockQuantity > 0;
            var bookresponse=_mapper.Map<BookResponse>(book);
            return bookresponse;
        }
        public List<BookResponse> SerachBooks(BookSearchRequest request)
        {
            var query = books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(x =>
                    x.Title.Contains(request.Search, StringComparison.OrdinalIgnoreCase) ||
                    x.ISBN.Contains(request.Search, StringComparison.OrdinalIgnoreCase));
            }
            if (request.CategoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == request.CategoryId.Value);
            }
            if (request.AuthorId.HasValue)
            {
                query = query.Where(x => x.AuthorId == request.AuthorId.Value);
            }
            if (request.IsAvailable.HasValue)
            {
                query = query.Where(x => x.IsAvailable == request.IsAvailable.Value);
            }

            if (request.PageNumber < 1)
            {
                request.PageNumber = 1;
            }

            if (request.PageSize < 1)
            {
                request.PageSize = 10;
            }

            var result = query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();
            var SearchedBooks= _mapper.Map<List<BookResponse>>(result);

            return SearchedBooks;
        }
        public BookSummaryResponse GetSummary()
        {
            var summary = new BookSummaryResponse
            {
                TotalBooks = books.Count,

                AvailableBooks = books.Count(x => x.IsAvailable),

                OutOfStockBooks = books.Count(x => x.StockQuantity == 0),

                BooksPerCategory = books.GroupBy(x => x.CategoryId).ToDictionary(x => x.Key, x => x.Count()),
                BooksPerAuthor = books.GroupBy(x => x.AuthorId).ToDictionary(x => x.Key, x => x.Count()),
                TotalInventoryValue = books.Sum(x => x.Price * x.StockQuantity)
            };


             
            return summary;
        }
    }
}
