using AutoMapper;
using MediatR;
using OutOfOffice.Shared.DTOs.Employee;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Application.Features.Employees.Requests.Queries;
using OutOfOffice.Shared.RequestFeatures;

namespace OutOfOffice.Application.Features.Employees.Handlers.Queries;

public class GetEmployeesRequestHandler : IRequestHandler<GetEmployeesRequest, (List<EmployeeDto>, MetaData)>
{
    private readonly IEmployeeRepository employeeRepository;
    private readonly IMapper mapper;

    public GetEmployeesRequestHandler(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        this.employeeRepository = employeeRepository;
        this.mapper = mapper;
    }

    public async Task<(List<EmployeeDto>, MetaData)> Handle(GetEmployeesRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        var employees = await employeeRepository.GetAllAsync(request.employeeParameters, false) ?? throw new Exception("Employee not found");

        var employeesDto = mapper.Map<List<EmployeeDto>>(employees);
        //return employeesDto;
    }
}