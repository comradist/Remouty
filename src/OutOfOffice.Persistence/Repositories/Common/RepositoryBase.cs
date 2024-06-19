using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyMind.Contract.Persistences;
using OutOfOffice.Persistence;

namespace MyMind.Persistence.Repositories.Common;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    private readonly RepositoryOutOfOfficeDbContext repositoryAppDbContext;

    protected RepositoryBase(RepositoryOutOfOfficeDbContext repositoryOutOfOfficeDbContext)
    {
        this.repositoryAppDbContext = repositoryOutOfOfficeDbContext;
    }

    public IQueryable<T> FindAll(bool trackChanges)
    {
        return !trackChanges ? repositoryAppDbContext.Set<T>().AsNoTracking() : repositoryAppDbContext.Set<T>();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
    {
        return !trackChanges ? repositoryAppDbContext.Set<T>().Where(expression).AsNoTracking() : repositoryAppDbContext.Set<T>().Where(expression);
    }

    public async Task CreateAsync(T entity)
    {
        await repositoryAppDbContext.Set<T>().AddAsync(entity);
    }

    public void Delete(T entity)
    {
        repositoryAppDbContext.Set<T>().Remove(entity);
    }

    public void Update(T entity)
    {
        repositoryAppDbContext.Set<T>().Update(entity);
    }

    public async Task SaveChangesAsync()
    {
        await repositoryAppDbContext.SaveChangesAsync();
    }

}