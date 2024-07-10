using OutOfOffice.Domain.Models.Entities;
using OutOfOffice.Shared.RequestFeatures;

namespace OutOfOffice.Contracts.Persistence;

public interface IEmployeeRepository : IGenericRepositoryManager<Employee, Guid>
{
    Task<Employee> GetEmployeeByIdAsync(Guid id, bool trackChanges);

    Task<PagedList<Employee>> GetEmployeesByParamAsync(EmployeeParameters employeeParameters, bool trackChanges);
}