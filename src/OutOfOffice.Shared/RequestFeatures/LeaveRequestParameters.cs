namespace OutOfOffice.Shared.RequestFeatures;

public class LeaveRequestParameters : RequestParameters
{
    public Guid? Id { get; set; }

    public Guid? EmployeeId { get; set; }

    public int? AbsenceReasonId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? StatusId { get; set; }
}