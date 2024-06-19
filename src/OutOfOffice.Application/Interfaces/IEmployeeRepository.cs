using OutOfOffice.Application.Contracts.Persistence;
using OutOfOffice.Domain.Models.Entities;

namespace OutOfOffice.Application.Interfaces;

public interface IEmployeeRepository : IGenericRepositoryManager<Employee, Guid>
{
}