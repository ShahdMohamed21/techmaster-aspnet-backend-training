using TrainngCenter.Api.DTOs.Instructors;

namespace TrainngCenter.Api.Services.Interfaces
{
    public interface IInstructorService
    {
        Task<List<InstructorResponse>> GetAllAsync();
        Task<InstructorResponse>? GetInstructorById(int Id);
        Task<(bool Success, string Message, InstructorResponse? Data)> CreateAsync(CreateInstructorRequest request);

        Task<(bool Success, string Message)>UpdateAsync(int id, UpdateInstructorRequest request);

      //  Task<List<TrackListItemResponse>?> GetTracksAsync(int id);


    }
}
