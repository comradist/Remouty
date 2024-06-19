using OutOfOffice.Application.Contracts.Persistence;
using OutOfOffice.Domain.Models.Entities;

namespace OutOfOffice.Application.Interfaces;

public interface IApprovalRequestRepository : IGenericRepositoryManager<ApprovalRequest, Guid>
{
}