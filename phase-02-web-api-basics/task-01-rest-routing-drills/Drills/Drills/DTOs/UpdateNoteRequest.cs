using System.ComponentModel.DataAnnotations;

namespace Drills.DTOs
{
    public class UpdateNoteRequest
    {
        [Required]
        public string Title { get; set; }

        public string Content { get; set; }
    }
}
