using MediatR;
using OutOfOffice.Shared.DTOs.Employee;
using OutOfOffice.Shared.DTOs.Project;

namespace OutOfOffice.Application.Features.Projects.Requests.Commands;

public class CreateProjectCommand : IRequest<ProjectDto>
{
    public CreateProjectDto ProjectDto { get; set; }
}