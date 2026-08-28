using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.DTOs.Categories
{
    public class UpdateCategoryRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; }
    }
}
