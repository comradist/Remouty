using OutOfOffice.Domain.Models.Entities;
using OutOfOffice.Shared.RequestFeatures;

namespace OutOfOffice.Contracts.Persistence;

public interface IApprovalRequestRepository : IGenericRepositoryManager<ApprovalRequest, Guid>
{
    Task<ApprovalRequest> GetApprovalRequestByIdAsync(Guid id, bool trackChanges);

    Task<PagedList<ApprovalRequest>> GetApprovalRequestsByParamAsync(ApprovalRequestParameters employeeParameters, bool trackChanges);

}