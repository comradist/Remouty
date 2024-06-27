using OutOfOffice.Domain.Models.Entities.LookUpTables;
using OutOfOffice.Shared.DTOs.Common;
using OutOfOffice.Shared.DTOs.Employee;

namespace OutOfOffice.Shared.DTOs.LeaveRequest;

public class LeaveRequestDto : BaseDto
{
    public EmployeeDto Employee { get; set; }

    public AbsenceReason AbsenceReason { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Comment { get; set; }

    public RequestStatus Status { get; set; }
}