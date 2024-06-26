using AutoMapper;
using MediatR;
using OutOfOffice.Shared.DTOs.Employee;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Application.Features.Employees.Requests.Queries;
using OutOfOffice.Shared.RequestFeatures;

namespace OutOfOffice.Application.Features.Employees.Handlers.Queries;

public class GetEmployeesWithQueryRequestHandler : IRequestHandler<GetEmployeesWithQueryRequest, (List<EmployeeDto>, MetaData)>
{
    private readonly IEmployeeRepository employeeRepository;
    private readonly IMapper mapper;

    public GetEmployeesWithQueryRequestHandler(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        this.employeeRepository = employeeRepository;
        this.mapper = mapper;
    }

    public async Task<(List<EmployeeDto>, MetaData)> Handle(GetEmployeesWithQueryRequest request, CancellationToken cancellationToken)
    {
        var test = new EmployeeParameters(){ FilterTerm = request.Query };
        var employees = await employeeRepository.GetEmployeesByParamAsync(false, test) ?? throw new Exception("Employee not found");

        var employeesDto = mapper.Map<List<EmployeeDto>>(employees);

        return (employeesDto, employees.MetaData);
    }
}