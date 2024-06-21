using MediatR;

namespace OutOfOffice.Application.Features.Employees.Requests.Commands;

public class DeleteEmployeeCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}