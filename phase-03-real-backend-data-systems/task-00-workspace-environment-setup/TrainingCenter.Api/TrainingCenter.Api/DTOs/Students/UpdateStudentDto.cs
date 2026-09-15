namespace TrainingCenter.Api.DTOs.Students
{
    public class UpdateStudentDto
    {
        // انا هنا مش حاطه كل ال field عشان بجرب بس جزء ال audit

        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
