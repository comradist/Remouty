using AutoMapper;
using OutOfOffice.Domain.Models.Entities;
using OutOfOffice.Identity.Models;
using OutOfOffice.Shared.DTOs.Employee;
using OutOfOffice.Shared.DTOs.Identity;

namespace MyMind.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Employee, EmployeeDto>().ReverseMap();
        CreateMap<Employee, CreateEmployeeDto>().ReverseMap();
        CreateMap<Employee, UpdateEmployeeDto>().ReverseMap();
        CreateMap<User, UserRegistrationDto>().ReverseMap();
        CreateMap<User, UserAuthenticationDto>().ReverseMap();
    }
}