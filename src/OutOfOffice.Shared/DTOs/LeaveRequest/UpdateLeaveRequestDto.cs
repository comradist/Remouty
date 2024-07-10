using OutOfOffice.Shared.DTOs.Common;

namespace OutOfOffice.Shared.DTOs.LeaveRequest;

public class UpdateLeaveRequestDto : BaseDto
{
    public Guid EmployeeId { get; set; }

    public int AbsenceReasonId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Comment { get; set; }

    public int StatusId { get; set; }
}