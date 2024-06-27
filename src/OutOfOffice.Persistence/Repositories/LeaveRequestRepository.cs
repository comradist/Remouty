using Microsoft.EntityFrameworkCore;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Domain.Models.Entities;
using OutOfOffice.Persistence.Extensions;
using OutOfOffice.Shared.RequestFeatures;

namespace OutOfOffice.Persistence.Repositories;

public class LeaveRequestRepository : GenericRepositoryManager<LeaveRequest, Guid>, ILeaveRequestRepository
{
    public LeaveRequestRepository(RepositoryOutOfOfficeDbContext repositoryOutOfOfficeDbContext) : base(repositoryOutOfOfficeDbContext)
    {
    }

    public async Task<LeaveRequest> GetLeaveRequestByIdAsync(Guid id, bool trackChanges)
    {
        return await GetByConditionAsync(x => x.Id.Equals(id), trackChanges);
    }

    public async Task<PagedList<LeaveRequest>> GetLeaveRequestsByParamAsync(LeaveRequestParameters leaveRequestParameters, bool trackChanges)
    {
        var LeaveRequests = await FindAll(false)
            .FilterAndSearch(leaveRequestParameters.FilterAndSearchTerm!)
            .Skip((leaveRequestParameters.PageNumber - 1) * leaveRequestParameters.PageSize)
            .Take(leaveRequestParameters.PageSize)
            .Sort(leaveRequestParameters.OrderBy!)
            .ToListAsync();

        return PagedList<LeaveRequest>.ToPagedList(LeaveRequests, leaveRequestParameters.PageNumber, leaveRequestParameters.PageSize);
    }
}