
using OutOfOffice.MVC.Models.Employee;
using OutOfOffice.MVC.Services.Base;

namespace OutOfOffice.MVC.Models.Project;

public class ProjectVM 
{
    public Guid Id { get; set; }

    public ProjectType ProjectType { get; set; }

    public DateTimeOffset StartDate { get; set; }

    public DateTimeOffset EndDate { get; set; }

    public EmployeeDto ProjectManager { get; set; }

    public string Comment { get; set; }

    public ProjectStatus Status { get; set; }

    public ICollection<EmployeeVM> Employees { get; set; } 

}