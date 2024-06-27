using Microsoft.EntityFrameworkCore;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Domain.Models.Entities;
using OutOfOffice.Persistence.Extensions;
using OutOfOffice.Shared.RequestFeatures;

namespace OutOfOffice.Persistence.Repositories;

public class ApprovalRequestRepository : GenericRepositoryManager<ApprovalRequest, Guid>, IApprovalRequestRepository
{
    public ApprovalRequestRepository(RepositoryOutOfOfficeDbContext repositoryOutOfOfficeDbContext) : base(repositoryOutOfOfficeDbContext)
    {
    }

    public async Task<ApprovalRequest> GetApprovalRequestByIdAsync(Guid id, bool trackChanges)
    {
        return await GetByConditionAsync(x => x.Id.Equals(id), trackChanges);
    }

    public async Task<PagedList<ApprovalRequest>> GetApprovalRequestsByParamAsync(ApprovalRequestParameters approvalRequestParameters, bool trackChanges)
    {
        var ApprovalRequests = await FindAll(false)
            .FilterAndSearch(approvalRequestParameters.FilterAndSearchTerm!)
            .Skip((approvalRequestParameters.PageNumber - 1) * approvalRequestParameters.PageSize)
            .Take(approvalRequestParameters.PageSize)
            .Sort(approvalRequestParameters.OrderBy!)
            .ToListAsync();

        return PagedList<ApprovalRequest>.ToPagedList(ApprovalRequests, approvalRequestParameters.PageNumber, approvalRequestParameters.PageSize);
    }

}