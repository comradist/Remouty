using MediatR;
using OutOfOffice.Shared.DTOs.Employee;

namespace OutOfOffice.Application.Features.Employees.Requests.Commands;

public class CreateEmployeeCommand : IRequest<EmployeeDto>
{
    public CreateEmployeeDto EmployeeDto { get; set; }
}