using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Domain.Models.Entities;

namespace OutOfOffice.Persistence.Repositories;

public class ApprovalRequestRepository : GenericRepositoryManager<ApprovalRequest, Guid>, IApprovalRequestRepository
{
    public ApprovalRequestRepository(RepositoryOutOfOfficeDbContext repositoryOutOfOfficeDbContext) : base(repositoryOutOfOfficeDbContext)
    {
    }
}