
namespace OutOfOffice.MVC.Models.Project;

public class CreateProjectVM
{
    public int ProjectTypeId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public Guid ProjectManagerId { get; set; }

    public string Comment { get; set; }
}