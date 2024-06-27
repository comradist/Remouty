using MediatR;
using OutOfOffice.Shared.DTOs.Employee;
using OutOfOffice.Shared.RequestFeatures;

namespace OutOfOffice.Application.Features.Employees.Requests.Queries;

public class GetEmployeesByParamRequest : IRequest<(List<EmployeeDto>, MetaData)>
{
    public EmployeeParameters employeeParameters { get; set; }
}