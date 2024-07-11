using AutoMapper;
using DemoApp.API.Models.Domain;
using DemoApp.API.Models.DTO;

namespace DemoApp.API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<AddStudentRequestDto, Student>().ReverseMap();
        }
    }
}
