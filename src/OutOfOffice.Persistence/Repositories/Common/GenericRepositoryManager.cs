using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Persistence.Repositories.Common;
using OutOfOffice.Shared.RequestFeatures;

namespace OutOfOffice.Persistence.Repositories;

public abstract class GenericRepositoryManager<T, K> : RepositoryBase<T>, IGenericRepositoryManager<T, K> where T : class
{
    protected GenericRepositoryManager(RepositoryOutOfOfficeDbContext repositoryOutOfOfficeDbContext) : base(repositoryOutOfOfficeDbContext)
    {
    }

    public async Task<T> GetByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges)
    {
        return await FindByCondition(expression, trackChanges).FirstOrDefaultAsync();
    }

    public async Task<PagedList<T>> GetAllAsync(EmployeeParameters employeeParameters, bool trackChanges)
    {
        var employees = await FindAll(false)
            .Skip((employeeParameters.PageNumber - 1) * employeeParameters.PageSize)
            .Take(employeeParameters.PageSize)
            .ToListAsync();

        return PagedList<T>.ToPagedList(employees, employeeParameters.PageNumber, employeeParameters.PageSize);
    }

    public async override Task CreateAsync(T entity)
    {
        await base.CreateAsync(entity);
        await SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        Update(entity);
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        Delete(entity);
        await SaveChangesAsync();
    }
}