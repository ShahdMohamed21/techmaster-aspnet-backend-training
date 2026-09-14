using TrainingCenter.Api.Entities.Payments;
using TrainingCenter.Api.Entities.Students;
using TrainingCenter.Api.Entities.Tracks;

namespace TrainingCenter.Api.Entities.Enrollments;

public class Enrollment
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int TrainingTrackId { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime EnrollmentDate { get; set; }

    public decimal? FinalGrade { get; set; }

    public Student Student { get; set; } = null!;

    public TrainingTrack TrainingTrack { get; set; }
    public PaymentSummary? PaymentSummary { get; set; }
}