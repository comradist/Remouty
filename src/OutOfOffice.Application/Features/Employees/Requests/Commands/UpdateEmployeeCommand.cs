using MediatR;
using OutOfOffice.Shared.DTOs.Employee;

namespace OutOfOffice.Application.Features.Employees.Requests.Commands;

public class UpdateEmployeeCommand : IRequest<Unit>
{
    public UpdateEmployeeDto UpdateEmployeeDto { get; set; }
}