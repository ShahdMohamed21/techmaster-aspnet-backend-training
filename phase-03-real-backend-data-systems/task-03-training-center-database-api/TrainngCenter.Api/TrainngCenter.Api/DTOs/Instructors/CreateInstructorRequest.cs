namespace TrainngCenter.Api.DTOs.Instructors
{
    public class CreateInstructorRequest
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Specialization { get; set; } = null!;
        public string? Bio { get; set; }
    }
}
