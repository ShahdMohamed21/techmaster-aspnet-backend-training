using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.DTOs.Authors
{
    public class CreateAuthorRequest
    {
        [Required]
        public string FullName { get; set; }
        public string Country { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}
