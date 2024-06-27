
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
        return await GetByConditionAsync(x => x.Id.Equals(id), trackChanges);
    }

    public async Task<PagedList<Employee>> GetEmployeesByParamAsync(EmployeeParameters employeeParameters, bool trackChanges)
    {
        var employees = await FindAll(false)
            .FilterAndSearch(employeeParameters.FilterAndSearchTerm!)
            .Skip((employeeParameters.PageNumber - 1) * employeeParameters.PageSize)
            .Take(employeeParameters.PageSize)
            .Sort(employeeParameters.OrderBy!)
            .ToListAsync();

        return PagedList<Employee>.ToPagedList(employees, employeeParameters.PageNumber, employeeParameters.PageSize);
    }



}