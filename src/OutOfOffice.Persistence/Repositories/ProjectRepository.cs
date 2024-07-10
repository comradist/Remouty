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
        return await FindByCondition(x => x.Id.Equals(id), trackChanges)
                .Include(x => x.ProjectEmployees)
                    .ThenInclude(x => x.Employee)
                .FirstOrDefaultAsync();
    }

    public async Task<PagedList<Project>> GetProjectsByParamAsync(ProjectParameters projectParameters, bool trackChanges)
    {
        // var projects = await FindAll(false)
        //     .Include(x => x.ProjectEmployees)
        //         .ThenInclude(x => x.Project)
        //     .FilterAndSearch(projectParameters.FilterAndSearchTerm!)
        //     .Skip((projectParameters.PageNumber - 1) * projectParameters.PageSize)
        //     .Take(projectParameters.PageSize)
        //     .Sort(projectParameters.OrderBy!)
        //     .ToListAsync();

        // Start with the base query
        IQueryable<Project> query = FindAll(trackChanges)
            .Include(x => x.ProjectEmployees)
                .ThenInclude(x => x.Employee);

        // Apply filtering and searching
        query = query.FilterAndSearch(projectParameters.FilterAndSearchTerm);

        // Apply sorting
        query = query.Sort(projectParameters.OrderBy);

        // Apply paging
        var projects = await query
            // .Skip((projectParameters.PageNumber - 1) * projectParameters.PageSize)
            // .Take(projectParameters.PageSize)
            .ToListAsync();

        // Return the paged list
        return PagedList<Project>.ToPagedList(projects, projectParameters.PageNumber, projectParameters.PageSize);
    }
}