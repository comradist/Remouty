using OutOfOffice.Domain.Models.Entities;

namespace OutOfOffice.Contracts.Persistence;

public interface IProjectRepository : IGenericRepositoryManager<Project, Guid>
{
}