using MediatR;
using OutOfOffice.Shared.DTOs.Project;

namespace OutOfOffice.Application.Features.Projects.Requests.Queries;

public class GetProjectByIdRequest : IRequest<ProjectDto>
{
    public Guid Id { get; set; }
}