using MediatR;
using OutOfOffice.Shared.DTOs.Employee;

namespace OutOfOffice.Application.Features.Employees.Requests.Queries;

public class GetEmployeesRequest : IRequest<List<EmployeeDto>>
{

}