namespace OutOfOffice.Shared.RequestFeatures;

public class ApprovalRequestParameters : RequestParameters
{
    public Guid? ApproverId { get; set; }

    public Guid? LeaveRequestId { get; set; }

    public int? StatusId { get; set; }

}