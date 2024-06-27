using AutoMapper;
using MediatR;
using OutOfOffice.Application.Features.Projects.Requests.Commands;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Domain.Models.Entities;

namespace OutOfOffice.Application.Features.Projects.Handlers.Commands;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Unit>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public DeleteProjectCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await repositoryManager.Project.GetProjectByIdAsync(request.Id, false) ?? throw new Exception("Project not found");

        await repositoryManager.Project.DeleteAsync(project);
        await repositoryManager.SaveChangesAsync();

        return Unit.Value;
    }
}