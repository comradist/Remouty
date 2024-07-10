using AutoMapper;
using MediatR;
using OutOfOffice.Shared.DTOs.Employee;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Application.Features.Employees.Requests.Queries;
using OutOfOffice.Shared.RequestFeatures;
using OutOfOffice.Shared.DTOs.Project;

namespace OutOfOffice.Application.Features.Employees.Handlers.Queries;

public class GetEmployeesByParamRequestHandler : IRequestHandler<GetEmployeesByParamRequest, (List<EmployeeDto>, MetaData)>
{
    private readonly IEmployeeRepository employeeRepository;
    private readonly IMapper mapper;

    public GetEmployeesByParamRequestHandler(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        this.employeeRepository = employeeRepository;
        this.mapper = mapper;
    }

    public async Task<(List<EmployeeDto>, MetaData)> Handle(GetEmployeesByParamRequest request, CancellationToken cancellationToken)
    {
        var pageListWithEmployee = await employeeRepository.GetEmployeesByParamAsync(request.employeeParameters, false);
        var employeesDto = mapper.Map<List<EmployeeDto>>(pageListWithEmployee);
        //var projectsDto = mapper.Map<List<ProjectDto>>(pageListWithEmployee.Item2);
        // foreach (var employeeDto in employeesDto)
        // {
        //     employeeDto.Projects = pageListWithEmployee.Item2.Where(x => x.Any(x => x.EmployeeId == employeeDto.Id)).ToList();
        // }
        return (employeesDto, pageListWithEmployee.MetaData);
    }
}