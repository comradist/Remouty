using AutoMapper;
using MediatR;
using OutOfOffice.Application.Features.Employees.Requests.Queries;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Shared.DTOs.Employee;

namespace OutOfOffice.Application.Features.Employees.Handlers.Queries;

public class GetEmployeeByIdRequestHandler : IRequestHandler<GetEmployeeByIdRequest, EmployeeDto>
{
    private readonly IEmployeeRepository employeeRepository;
    private readonly IMapper mapper;

    public GetEmployeeByIdRequestHandler(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        this.employeeRepository = employeeRepository;
        this.mapper = mapper;
    }

    public async Task<EmployeeDto> Handle(GetEmployeeByIdRequest request, CancellationToken cancellationToken)
    {
        var employee = await employeeRepository.GetEmployeeByIdAsync(request.Id, false) ?? throw new Exception("Employee not found");

        var employeeDto = mapper.Map<EmployeeDto>(employee);

        return employeeDto;
    }
}