using AutoMapper;
using TrainingCenter.Api.DTOs.Students;
using TrainingCenter.Api.Entities;
using TrainngCenter.Api.DTOs.Instructors;
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
    }
}
