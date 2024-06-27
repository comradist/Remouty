using OutOfOffice.Domain.Models.Entities.LookUpTables;
using OutOfOffice.Shared.DTOs.Common;
using OutOfOffice.Shared.DTOs.Employee;
using OutOfOffice.Shared.DTOs.LeaveRequest;

namespace OutOfOffice.Shared.DTOs.ApprovalRequest;

public class ApprovalRequestDto : BaseDto
{
    public Guid ApproverId { get; set; }
    
    public EmployeeDto? Approver { get; set; }

    public LeaveRequestDto LeaveRequest { get; set; }

    public RequestStatus Status { get; set; }

    public string? Comment { get; set; }
}