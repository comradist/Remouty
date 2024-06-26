using AutoMapper;
using OutOfOffice.MVC.Models.Employee;
using OutOfOffice.MVC.Models.Identity;
using OutOfOffice.MVC.Services.Base;

namespace OutOfOffice.MVC.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegistrationDto, UserRegistrationVM>().ReverseMap();
            CreateMap<UserAuthenticationDto, UserAuthenticationVM>().ReverseMap();
            CreateMap<TokenDto, TokenVM>().ReverseMap();
            CreateMap<TokenVM, UserAuthenticationDto>().ReverseMap();
            CreateMap<EmployeeDto, EmployeeVM>().ReverseMap();
        }
    }

}
