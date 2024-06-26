using OutOfOffice.Domain.Models.Entities.Common;
using OutOfOffice.Domain.Models.Entities.LookUpTables;

namespace OutOfOffice.Domain.Models.Entities;

public class Project : BaseEntity
{
    public Guid Id { get; set; }
    public int ProjectTypeId { get; set; }
    public ProjectType ProjectType { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public Guid ProjectManagerId { get; set; }
    public Employee ProjectManager { get; set; }

    public string Comment { get; set; }
    
    public int StatusId { get; set; }
    public ProjectStatus Status { get; set; }

    public ICollection<ProjectEmployee> ProjectEmployees { get; set; } = new List<ProjectEmployee>();
}