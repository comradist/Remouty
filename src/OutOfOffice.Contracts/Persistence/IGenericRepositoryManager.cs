using System.Linq.Expressions;

namespace OutOfOffice.Contracts.Persistence;

public interface IGenericRepositoryManager<T, K>
{
    Task<T> GetByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges);

    Task<IEnumerable<T>> GetAllAsync(bool trackChanges);

    Task CreateAsync(T entity);

    //Task<bool> ExistsAsync(K id);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);
}

