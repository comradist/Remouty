using OutOfOffice.Domain.Models.Entities.Common;
using OutOfOffice.Domain.Models.Entities.LookUpTables;

namespace OutOfOffice.Domain.Models.Entities;

public class LeaveRequest : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; }

    public int AbsenceReasonId { get; set; }
    public AbsenceReason AbsenceReason { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Comment { get; set; }
    
    public int StatusId { get; set; }
    public RequestStatus Status { get; set; }
}