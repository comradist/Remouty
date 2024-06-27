using Microsoft.EntityFrameworkCore;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Domain.Models.Entities;
using OutOfOffice.Persistence.Extensions;
using OutOfOffice.Shared.RequestFeatures;

namespace OutOfOffice.Persistence.Repositories;

public class ProjectRepository : GenericRepositoryManager<Project, Guid>, IProjectRepository
{
    public ProjectRepository(RepositoryOutOfOfficeDbContext repositoryOutOfOfficeDbContext) : base(repositoryOutOfOfficeDbContext)
    {
    }

    public async Task<Project> GetProjectByIdAsync(Guid id, bool trackChanges)
    {
        return await GetByConditionAsync(x => x.Id.Equals(id), trackChanges);
    }

    public async Task<PagedList<Project>> GetProjectsByParamAsync(ProjectParameters projectParameters, bool trackChanges)
    {
        var projects = await FindAll(false)
            .FilterAndSearch(projectParameters.FilterAndSearchTerm!)
            .Skip((projectParameters.PageNumber - 1) * projectParameters.PageSize)
            .Take(projectParameters.PageSize)
            .Sort(projectParameters.OrderBy!)
            .ToListAsync();

        return PagedList<Project>.ToPagedList(projects, projectParameters.PageNumber, projectParameters.PageSize);
    }
}