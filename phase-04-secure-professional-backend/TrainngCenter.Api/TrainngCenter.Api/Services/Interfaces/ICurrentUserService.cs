namespace TrainngCenter.Api.Services.Interfaces
{
    public interface ICurrentUserService
    {
        int UserId { get; }

        int? StudentId { get; }

        int? InstructorId { get; }

        string? Role { get; }
    }
}