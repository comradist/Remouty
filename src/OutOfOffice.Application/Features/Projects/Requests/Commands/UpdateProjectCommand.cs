using MediatR;
using OutOfOffice.Shared.DTOs.Employee;
using OutOfOffice.Shared.DTOs.Project;

namespace OutOfOffice.Application.Features.Projects.Requests.Commands;

public class UpdateProjectCommand : IRequest<Unit>
{
    public UpdateProjectDto ProjectDto { get; set; }
}