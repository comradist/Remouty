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
        CreateMap<Project, ProjectDto>().ReverseMap();
        CreateMap<Project, CreateProjectDto>().ReverseMap();
        CreateMap<Project, UpdateProjectDto>().ReverseMap();

        CreateMap<ApprovalRequest, ApprovalRequestDto>().ReverseMap();
        CreateMap<ApprovalRequest, CreateApprovalRequestDto>().ReverseMap();
        CreateMap<ApprovalRequest, UpdateApprovalRequestDto>().ReverseMap();

        CreateMap<LeaveRequest, LeaveRequestDto>().ReverseMap();
        CreateMap<LeaveRequest, CreateLeaveRequestDto>().ReverseMap();
        CreateMap<LeaveRequest, UpdateLeaveRequestDto>().ReverseMap();

        CreateMap<Employee, EmployeeDto>().ReverseMap();
        CreateMap<Employee, CreateEmployeeDto>().ReverseMap();
        CreateMap<Employee, UpdateEmployeeDto>().ReverseMap();

        CreateMap<User, UserRegistrationDto>().ReverseMap();
        CreateMap<User, UserAuthenticationDto>().ReverseMap();
    }
}