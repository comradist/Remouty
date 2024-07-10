using AutoMapper;
using MediatR;
using OutOfOffice.Shared.DTOs.Project;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Application.Features.Projects.Requests.Queries;
using OutOfOffice.Shared.RequestFeatures;
namespace OutOfOffice.Application.Features.Projects.Handlers.Queries;

public class GetProjectsByParamRequestHandler : IRequestHandler<GetProjectsByParamRequest, (List<ProjectDto>, MetaData)>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public GetProjectsByParamRequestHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<(List<ProjectDto>, MetaData)> Handle(GetProjectsByParamRequest request, CancellationToken cancellationToken)
    {
        var pageListWithProject = await repositoryManager.Project.GetProjectsByParamAsync(request.projectParameters, false) ?? throw new Exception("Project not found");

        var projectsDto = mapper.Map<List<ProjectDto>>(pageListWithProject);

        return (projectsDto, pageListWithProject.MetaData);
    }
}