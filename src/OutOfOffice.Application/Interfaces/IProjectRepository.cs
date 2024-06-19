using OutOfOffice.Application.Contracts.Persistence;
using OutOfOffice.Domain.Models.Entities;

namespace OutOfOffice.Application.Interfaces;

public interface IProjectRepository : IGenericRepositoryManager<Project, Guid>
{
}