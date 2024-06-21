using OutOfOffice.Domain.Models.Entities;

namespace OutOfOffice.Contracts.Persistence;

public interface ILeaveRequestRepository : IGenericRepositoryManager<LeaveRequest, Guid>
{
}