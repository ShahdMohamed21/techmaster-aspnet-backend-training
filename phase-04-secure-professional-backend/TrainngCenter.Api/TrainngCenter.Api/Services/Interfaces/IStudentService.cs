using TrainingCenter.Api.DTOs.Students;
using TrainngCenter.Api.DTOs.Students;

namespace TrainingCenter.Api.Services.Interfaces
{
    public interface IStudentService
    {
        Task<object> GetAllAsync(string? search,bool? isActive,int page,int pageSize);

        Task<StudentDetailsResponse?> GetByIdAsync(int id);

        Task<(bool Success, string Message, StudentDetailsResponse? Data)>CreateAsync(CreateStudentRequest request);

        Task<(bool Success, string Message)> UpdateAsync(int id, UpdateStudentRequest request);

        Task<(bool Success, string Message)>DeleteAsync(int id);

        Task<StudentDetailsResponse?> GetMyProfileAsync(int studentId);

        Task<List<MyEnrollmentResponse>> GetMyEnrollmentsAsync(int studentId);

        Task<List<MyPaymentResponse>> GetMyPaymentsAsync(int studentId);

        Task<(bool Success, string Message)> UpdateMyProfileAsync(int studentId,UpdateMyProfileRequest request);



    }
}