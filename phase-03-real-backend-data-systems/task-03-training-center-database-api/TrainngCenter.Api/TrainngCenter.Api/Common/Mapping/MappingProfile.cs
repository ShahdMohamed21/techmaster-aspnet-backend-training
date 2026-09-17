using AutoMapper;
using TrainingCenter.Api.DTOs.Students;
using TrainingCenter.Api.Entities;
using TrainngCenter.Api.DTOs.Enrollments;
using TrainngCenter.Api.DTOs.Instructors;
using TrainngCenter.Api.DTOs.Payments;
using TrainngCenter.Api.DTOs.Tracks;
namespace TrainngCenter.Api.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateInstructorRequest, Instructor>();
        CreateMap<Instructor, InstructorResponse>();
        CreateMap<UpdateInstructorRequest, Instructor>();

        CreateMap<CreateStudentRequest, Student>();
        CreateMap<UpdateStudentRequest, Student>();
        CreateMap<Student, StudentListItemResponse>();
        CreateMap<Student, StudentDetailsResponse>();

        CreateMap<CreateTrackRequest,TrainingTrack>();
        CreateMap<UpdateTrackRequest, TrainingTrack>();
        CreateMap<TrainingTrack,TrackDetailsResponse>();
        CreateMap<TrainingTrack, TrackListItemResponse>();

        CreateMap<CreateEnrollmentRequest, Enrollment>();
        CreateMap<Enrollment, EnrollmentListItemResponse>();

        CreateMap<Enrollment, EnrollmentDetailsResponse>()
            .ForMember(
                dest => dest.StudentName,
                opt => opt.MapFrom(src => src.Student.FullName))
            .ForMember(
                dest => dest.TrainingTrackTitle,
                opt => opt.MapFrom(src => src.TrainingTrack.Title));

        CreateMap<CreatePaymentRequest, Payment>();
        CreateMap<Payment, PaymentResponse>();
    }
}
