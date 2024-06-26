using MediatR;
using OutOfOffice.Shared.DTOs.Employee;

namespace OutOfOffice.Application.Features.Employees.Requests.Queries;

public class GetEmployeeByIdRequest : IRequest<EmployeeDto>
{
    public Guid Id { get; set; }
}