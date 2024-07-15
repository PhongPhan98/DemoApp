using AutoMapper;
using DemoApp.API.Models.Domain;
using DemoApp.API.Models.DTO.Classes;
using DemoApp.API.Models.DTO.Students;
using DemoApp.API.Models.DTO.Teachers;

namespace DemoApp.API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<AddStudentRequestDto, Student>().ReverseMap();

            CreateMap<Class, ClassDto>().ReverseMap();
            CreateMap<AddClassRequestDto, Class>().ReverseMap();

            CreateMap<Teacher, TeacherDto>().ReverseMap();
            CreateMap<AddTeacherRequestDto, Teacher>().ReverseMap();
        }
    }
}
