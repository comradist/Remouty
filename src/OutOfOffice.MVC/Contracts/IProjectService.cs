using OutOfOffice.MVC.Models.Project;
using OutOfOffice.MVC.Shared.RequestFeatures;

namespace OutOfOffice.MVC.Contracts;

public interface IProjectService
{
    Task<ProjectIndexVM> GetAllProjectsAsync(ProjectParameters ProjectParameters);
    Task<ProjectVM> CreateProjectAsync(CreateProjectVM Project);
}