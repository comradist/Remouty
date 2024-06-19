using OutOfOffice.Domain.Models.Entities.LookUpTables;

namespace OutOfOffice.Domain.Models.Entities;

public class ApprovalRequest
{
    public Guid Id { get; set; }

    public Guid ApproverId { get; set; }
    public Employee Approver { get; set; }

    public Guid LeaveRequestId { get; set; }
    public LeaveRequest LeaveRequest { get; set; }

    public int StatusId { get; set; }
    public RequestStatus Status { get; set; }

    public string Comment { get; set; }
}
