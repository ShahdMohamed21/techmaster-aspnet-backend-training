namespace TrainingCenter.Api.DTOs.Students;

public class StudentResponseDto
{
    // انا هنا مش حاطه كل ال field عشان بجرب بس جزء ال audit
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}