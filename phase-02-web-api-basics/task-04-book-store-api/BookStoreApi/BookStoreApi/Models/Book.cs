using System.ComponentModel.DataAnnotations;
using System.Net;

namespace BookStoreApi.Models
{
    public class Book
    {
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ISBN { get; set; }
        public string PublishedYear { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int AuthorId { get; set;}
        public int CategoryId { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
