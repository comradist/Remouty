namespace OutOfOffice.Contracts.Persistence;

public interface IRepositoryManager
{
    IProjectRepository Project { get; }

    ILeaveRequestRepository LeaveRequest { get; }

    IApprovalRequestRepository ApprovalRequest { get; }

    IEmployeeRepository Employee { get; }

    Task SaveChangesAsync();
}