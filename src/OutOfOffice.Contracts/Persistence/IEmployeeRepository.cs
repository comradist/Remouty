using OutOfOffice.Domain.Models.Entities;

namespace OutOfOffice.Contracts.Persistence;

public interface IEmployeeRepository : IGenericRepositoryManager<Employee, Guid>
{
    Task<Employee> GetEmployeeByIdAsync(Guid id, bool trackChanges);

    Task SaveChangesAsync();
}