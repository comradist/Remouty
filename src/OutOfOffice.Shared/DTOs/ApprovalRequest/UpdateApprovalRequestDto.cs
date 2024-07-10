using OutOfOffice.Shared.DTOs.Common;

namespace OutOfOffice.Shared.DTOs.ApprovalRequest;

public class UpdateApprovalRequestDto : BaseDto
{
    public Guid? ApproverId { get; set; }

    public Guid LeaveRequestId { get; set; }

    public int StatusId { get; set; }

    public string? Comment { get; set; }
}