using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Country { get; set; }
        public DateOnly? BirthDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
