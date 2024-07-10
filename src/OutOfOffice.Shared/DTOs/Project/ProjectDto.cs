using OutOfOffice.Domain.Models.Entities;
using OutOfOffice.Domain.Models.Entities.LookUpTables;
using OutOfOffice.Shared.DTOs.Common;
using OutOfOffice.Shared.DTOs.Employee;

namespace OutOfOffice.Shared.DTOs.Project;

public class ProjectDto : BaseDto
{
    public ProjectType ProjectType { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public EmployeeDto ProjectManager { get; set; }

    public string Comment { get; set; }

    public ProjectStatus Status { get; set; }

    public ICollection<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();

}