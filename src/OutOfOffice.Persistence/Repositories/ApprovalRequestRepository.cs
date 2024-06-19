
using OutOfOffice.Application.Interfaces;
using OutOfOffice.Domain.Models.Entities;

namespace OutOfOffice.Persistence.Repositories;

public class ApprovalRequestRepository : GenericRepositoryManager<ApprovalRequest, Guid>, IApprovalRequestRepository
{
    public ApprovalRequestRepository(RepositoryOutOfOfficeDbContext repositoryOutOfOfficeDbContext) : base(repositoryOutOfOfficeDbContext)
    {
    }
}