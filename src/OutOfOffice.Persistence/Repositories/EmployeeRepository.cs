
using OutOfOffice.Application.Interfaces;
using OutOfOffice.Domain.Models.Entities;

namespace OutOfOffice.Persistence.Repositories;

public class EmployeeRepository : GenericRepositoryManager<Employee, Guid>, IEmployeeRepository
{
    public EmployeeRepository(RepositoryOutOfOfficeDbContext repositoryOutOfOfficeDbContext) : base(repositoryOutOfOfficeDbContext)
    {
    }
}