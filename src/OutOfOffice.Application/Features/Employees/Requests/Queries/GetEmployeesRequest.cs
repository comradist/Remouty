using MediatR;
using OutOfOffice.Shared.DTOs.Employee;

namespace OutOfOffice.Application.Feature.Request.Queries;

public class GetEmployeesRequest : IRequest<List<EmployeeDto>>
{

}