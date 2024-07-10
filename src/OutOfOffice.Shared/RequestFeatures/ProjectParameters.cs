using OutOfOffice.Domain.Models.Entities;

namespace OutOfOffice.Shared.RequestFeatures;

public class ProjectParameters : RequestParameters
{
    public Guid? Id { get; set; }

    public int? ProjectTypeId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public Guid? ProjectManagerId { get; set; }

    public int? StatusId { get; set; }
}