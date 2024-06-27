using MediatR;

namespace OutOfOffice.Application.Features.Projects.Requests.Commands;

public class DeleteProjectCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}