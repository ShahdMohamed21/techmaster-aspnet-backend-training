using BookStoreApi.DTOs.Authors;
using BookStoreApi.DTOs.Books;

namespace BookStoreApi.Services
{
    public interface IBookService
    {
        public BookResponse CreateBook(CreateBookRequest request);
        public List<BookResponse> GetAllBooks();
        public BookResponse UpdateBook(int id, UpdateBookRequest request);
        public bool DeleteBook(int id);
        public BookResponse? GetBookById(int id);
       public List<BookResponse> SerachBooks(BookSearchRequest request);
       public BookSummaryResponse GetSummary();
    }
}
