using AutoMapper;
using MediatR;
using OutOfOffice.Application.Feature.Request.Queries;
using OutOfOffice.Shared.DTOs.Employee;
using OutOfOffice.Contracts.Persistence;

namespace OutOfOffice.Application.Features.Employees.Handlers.Queries;

public class GetEmployeesRequestHandler : IRequestHandler<GetEmployeesRequest, List<EmployeeDto>>
{
    private readonly IEmployeeRepository employeeRepository;
    private readonly IMapper mapper;

    public GetEmployeesRequestHandler(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        this.employeeRepository = employeeRepository;
        this.mapper = mapper;
    }

    public async Task<List<EmployeeDto>> Handle(GetEmployeesRequest request, CancellationToken cancellationToken)
    {
        var employees = await employeeRepository.GetAllAsync(false) ?? throw new Exception("Employee not found");

        var employeesDto = mapper.Map<List<EmployeeDto>>(employees);

        return employeesDto;
    }
}