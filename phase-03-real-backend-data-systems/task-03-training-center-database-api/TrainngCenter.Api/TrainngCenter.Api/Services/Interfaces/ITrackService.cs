using TrainingCenter.Api.DTOs.Students;
using TrainngCenter.Api.DTOs.Tracks;

namespace TrainngCenter.Api.Services.Interfaces
{
    public interface ITrackService
    {
        Task<List<TrackListItemResponse>> GetAllAsync(string? Keyword,string? level,string?status,int? instructorid);
        Task<TrackDetailsResponse?> GetByIdAsync(int id);
        Task<(bool success,string message, TrackDetailsResponse? Data)> CreateAsync(CreateTrackRequest request);
        Task<(bool success, string message)> UpdateAsync(int id , UpdateTrackRequest request);
        Task<(bool success, string message)> DeleteAsync(int id);
        Task<List<StudentListItemResponse>?> GetStudentsAsync(int id);



    }
}
