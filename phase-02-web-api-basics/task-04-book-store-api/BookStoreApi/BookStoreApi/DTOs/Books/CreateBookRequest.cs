using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.DTOs.Books
{
    public class CreateBookRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string ISBN { get; set; }
        public int PublishedYear { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
    }
}
