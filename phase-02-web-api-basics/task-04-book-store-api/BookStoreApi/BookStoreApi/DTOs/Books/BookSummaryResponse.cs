namespace BookStoreApi.DTOs.Books
{
    public class BookSummaryResponse
    {
        public int TotalBooks { get; set; }
        public int AvailableBooks { get; set; }
        public int OutOfStockBooks { get; set; }
        public Dictionary<int, int> BooksPerCategory { get; set; }
        public Dictionary<int, int> BooksPerAuthor { get; set; }
        public decimal TotalInventoryValue { get; set; }
    }
}
