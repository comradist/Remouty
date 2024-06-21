using MediatR;
using OutOfOffice.Shared.DTOs.Employee;

namespace OutOfOffice.Application.Feature.Request.Queries;

public class GetEmployeeByIdRequest : IRequest<EmployeeDto>
{
    public Guid Id { get; set; }
}