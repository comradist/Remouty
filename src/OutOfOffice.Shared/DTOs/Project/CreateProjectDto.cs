using OutOfOffice.Shared.DTOs.Common;

namespace OutOfOffice.Shared.DTOs.Project;

public class CreateProjectDto
{
    public int ProjectTypeId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public Guid ProjectManagerId { get; set; }

    public string Comment { get; set; }

    public int StatusId { get; set; }
}