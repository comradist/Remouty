using OutOfOffice.Domain.Models.Entities;

namespace OutOfOffice.Contracts.Persistence;

public interface IApprovalRequestRepository : IGenericRepositoryManager<ApprovalRequest, Guid>
{
}