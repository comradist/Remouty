using MediatR;
using OutOfOffice.Shared.DTOs.Project;
using OutOfOffice.Shared.RequestFeatures;

namespace OutOfOffice.Application.Features.Projects.Requests.Queries;

public class GetProjectsByParamRequest : IRequest<(List<ProjectDto>, MetaData)>
{
    public ProjectParameters projectParameters { get; set; }
}