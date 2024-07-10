using Microsoft.EntityFrameworkCore;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Domain.Models.Entities;
using OutOfOffice.Persistence.Extensions;
using OutOfOffice.Shared.RequestFeatures;

namespace OutOfOffice.Persistence.Repositories;

public class EmployeeRepository : GenericRepositoryManager<Employee, Guid>, IEmployeeRepository
{
    public EmployeeRepository(RepositoryOutOfOfficeDbContext repositoryOutOfOfficeDbContext) : base(repositoryOutOfOfficeDbContext)
    {

    }

    public async Task<Employee> GetEmployeeByIdAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(x => x.Id.Equals(id), trackChanges)
                .Include(x => x.PeoplePartner)
                .Include(x => x.ProjectEmployees)
                    .ThenInclude(x => x.Project)
                .FirstOrDefaultAsync();
    }

    public async Task<PagedList<Employee>> GetEmployeesByParamAsync(EmployeeParameters employeeParameters, bool trackChanges)
    {
        // var employees = await FindAll(false)
        //     .Include(x => x.PeoplePartner)
        //     .Include(x => x.ProjectEmployees)
        //         .ThenInclude(x => x.Project)
        //     .FilterAndSearch(employeeParameters.FilterAndSearchTerm!)
        //     .Skip((employeeParameters.PageNumber - 1) * employeeParameters.PageSize)
        //     .Take(employeeParameters.PageSize)
        //     .Sort(employeeParameters.OrderBy!)
        //     .ToListAsync();


        // Start with the base query
        IQueryable<Employee> query = FindAll(trackChanges)
            .Include(x => x.PeoplePartner)
            .Include(x => x.ProjectEmployees)
                .ThenInclude(x => x.Project);

        // Apply filtering and searching
        query = query.FilterAndSearch(employeeParameters.FilterAndSearchTerm);

        // Apply sorting
        query = query.Sort(employeeParameters.OrderBy);

        // Apply paging
        var employees = await query
            // .Skip((employeeParameters.PageNumber - 1) * employeeParameters.PageSize)
            // .Take(employeeParameters.PageSize)
            .ToListAsync();

        // Return the paged list
        return PagedList<Employee>.ToPagedList(employees, employeeParameters.PageNumber, employeeParameters.PageSize);
    }


}