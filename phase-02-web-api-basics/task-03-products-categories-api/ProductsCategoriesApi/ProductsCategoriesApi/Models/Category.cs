using System.ComponentModel.DataAnnotations;

namespace ProductsCategoriesApi.Models
{
    public class Category
    {
        [Required]
        public int Categoryd { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    }
}
