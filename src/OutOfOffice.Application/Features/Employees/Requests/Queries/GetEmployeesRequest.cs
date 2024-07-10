using MediatR;
using OutOfOffice.Shared.DTOs.Employee;
using OutOfOffice.Shared.RequestFeatures;

namespace OutOfOffice.Application.Features.Employees.Requests.Queries;

public class GetEmployeesRequest : IRequest<(List<EmployeeDto>, MetaData)>
{
    public EmployeeParameters employeeParameters { get; set; }
}