using AutoMapper;
using MediatR;
using OutOfOffice.Application.Features.Projects.Requests.Commands;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Domain.Models.Entities;

namespace OutOfOffice.Application.Features.Projects.Handlers.Commands;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Unit>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public UpdateProjectCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = mapper.Map<Project>(request.ProjectDto);

        await repositoryManager.Project.UpdateAsync(project);
        await repositoryManager.SaveChangesAsync();

        return Unit.Value;

    }
}