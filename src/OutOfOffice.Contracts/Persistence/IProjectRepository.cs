using OutOfOffice.Domain.Models.Entities;
using OutOfOffice.Shared.RequestFeatures;

namespace OutOfOffice.Contracts.Persistence;

public interface IProjectRepository : IGenericRepositoryManager<Project, Guid>
{
    Task<Project> GetProjectByIdAsync(Guid id, bool trackChanges);

    Task<PagedList<Project>> GetProjectsByParamAsync(ProjectParameters employeeParameters, bool trackChanges);

}