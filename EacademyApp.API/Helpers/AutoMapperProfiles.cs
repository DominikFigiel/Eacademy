using AutoMapper;
using EacademyApp.API.Dtos;
using EacademyApp.API.Models;
 namespace EacademyApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Student, StudentForListDto>();
            CreateMap<Student, StudentForDetailedDto>();
            CreateMap<CourseStudent, CourseStudentForDetailedDto>();
        }
    }
}
