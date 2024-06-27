using AutoMapper;
using MediatR;
using OutOfOffice.Application.Features.Projects.Requests.Queries;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Shared.DTOs.Project;

namespace OutOfOffice.Application.Features.Projects.Handlers.Queries;

public class GetProjectByIdRequestHandler : IRequestHandler<GetProjectByIdRequest, ProjectDto>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public GetProjectByIdRequestHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<ProjectDto> Handle(GetProjectByIdRequest request, CancellationToken cancellationToken)
    {
        var Project = await repositoryManager.Project.GetProjectByIdAsync(request.Id, false) ?? throw new Exception("Project not found");

        var ProjectDto = mapper.Map<ProjectDto>(Project);

        return ProjectDto;
    }
}