using MediatR;
using OutOfOffice.Shared.DTOs.Employee;
using OutOfOffice.Shared.RequestFeatures;

namespace OutOfOffice.Application.Features.Employees.Requests.Queries;

public class GetEmployeesWithQueryRequest : IRequest<(List<EmployeeDto>, MetaData)>
{
    public string Query { get; set; }
}