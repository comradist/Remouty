using AutoMapper;
using OutOfOffice.MVC.Models.Employee;
using OutOfOffice.MVC.Models.Identity;
using OutOfOffice.MVC.Models.Project;
using OutOfOffice.MVC.Services.Base;

namespace OutOfOffice.MVC.Profiles
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<ProjectDto, ProjectVM>().ReverseMap();
                
            CreateMap<CreateProjectDto, CreateProjectVM>().ReverseMap();
            CreateMap<ProjectVM, CreateProjectDto>().ReverseMap();

            CreateMap<CreateEmployeeVM, CreateEmployeeDto>().ReverseMap();
            CreateMap<EmployeeDto, EmployeeVM>().ReverseMap();
            CreateMap<EmployeeVM, CreateEmployeeDto>().ReverseMap();

            CreateMap<UserRegistrationDto, UserRegistrationVM>().ReverseMap();
            CreateMap<UserAuthenticationDto, UserAuthenticationVM>().ReverseMap();
            CreateMap<TokenDto, TokenVM>().ReverseMap();
            CreateMap<TokenVM, UserAuthenticationDto>().ReverseMap();
        }
    }

}
