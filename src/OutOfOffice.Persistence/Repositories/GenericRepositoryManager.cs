using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyMind.Persistence.Repositories.Common;
using OutOfOffice.Application.Contracts.Persistence;
using OutOfOffice.Persistence;

namespace MyMind.Persistence.Repositories;

public abstract class GenericRepositoryManager<T, K> : RepositoryBase<T>, IGenericRepositoryManager<T, K> where T : class
{
    protected GenericRepositoryManager(RepositoryOutOfOfficeDbContext repositoryOutOfOfficeDbContext) : base(repositoryOutOfOfficeDbContext)
    {
    }

    public async Task<T> Get(Expression<Func<T, bool>> expression, bool trackChanges)
    {
        return await FindByCondition(expression, trackChanges).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(bool trackChanges)
    {
        return await FindAll(false).ToListAsync();
    }

    public async Task CreateAsync(T entity)
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