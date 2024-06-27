using AutoMapper;
using MediatR;
using OutOfOffice.Application.Features.Projects.Requests.Commands;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Domain.Models.Entities;
using OutOfOffice.Shared.DTOs.Project;

namespace OutOfOffice.Application.Features.projects.Handlers.Commands;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public CreateProjectCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = mapper.Map<Project>(request.ProjectDto);

        await repositoryManager.Project.CreateAsync(project);
        project = await repositoryManager.Project.GetProjectByIdAsync(project.Id, false);

        var projectDto = mapper.Map<ProjectDto>(project);

        return projectDto;
    }
}
