
using Microsoft.EntityFrameworkCore;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Domain.Models.Entities;

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

}