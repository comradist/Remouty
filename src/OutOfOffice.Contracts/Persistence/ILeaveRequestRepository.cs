using OutOfOffice.Domain.Models.Entities;
using OutOfOffice.Shared.RequestFeatures;

namespace OutOfOffice.Contracts.Persistence;

public interface ILeaveRequestRepository : IGenericRepositoryManager<LeaveRequest, Guid>
{
    Task<LeaveRequest> GetLeaveRequestByIdAsync(Guid id, bool trackChanges);

    Task<PagedList<LeaveRequest>> GetLeaveRequestsByParamAsync(LeaveRequestParameters employeeParameters, bool trackChanges);

}