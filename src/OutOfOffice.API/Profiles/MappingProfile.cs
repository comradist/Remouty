using AutoMapper;
using OutOfOffice.Domain.Models.Entities;
using OutOfOffice.Identity.Models;
using OutOfOffice.Shared.DTOs.ApprovalRequest;
using OutOfOffice.Shared.DTOs.Employee;
using OutOfOffice.Shared.DTOs.Identity;
using OutOfOffice.Shared.DTOs.LeaveRequest;
using OutOfOffice.Shared.DTOs.Project;

namespace OutOfOffice.API.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProjectDto, Project>().ReverseMap()
            .ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.ProjectEmployees.Select(pe => pe.Employee).ToList()));

        CreateMap<Project, CreateProjectDto>().ReverseMap();
        CreateMap<Project, UpdateProjectDto>().ReverseMap();

        CreateMap<ApprovalRequest, ApprovalRequestDto>().ReverseMap();
        CreateMap<ApprovalRequest, CreateApprovalRequestDto>().ReverseMap();
        CreateMap<ApprovalRequest, UpdateApprovalRequestDto>().ReverseMap();

        CreateMap<LeaveRequest, LeaveRequestDto>().ReverseMap();
        CreateMap<LeaveRequest, CreateLeaveRequestDto>().ReverseMap();
        CreateMap<LeaveRequest, UpdateLeaveRequestDto>().ReverseMap();

        CreateMap<EmployeeDto, Employee>().ReverseMap()
            .ForMember(dest => dest.Projects, opt => opt.MapFrom(src => src.ProjectEmployees.Select(pe => pe.Project).ToList()));

        CreateMap<Employee, CreateEmployeeDto>().ReverseMap();
        CreateMap<Employee, UpdateEmployeeDto>().ReverseMap();

        CreateMap<User, UserRegistrationDto>().ReverseMap();
        CreateMap<User, UserAuthenticationDto>().ReverseMap();
    }
}