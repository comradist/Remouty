using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using OutOfOffice.Domain.Models.Entities;
using OutOfOffice.Persistence.Extensions.Utility;

namespace OutOfOffice.Persistence.Extensions;

public static class RepositoryApprovalRequestExtensions
{
    public static IQueryable<ApprovalRequest> FilterAndSearch(this IQueryable<ApprovalRequest> approvalRequests, string filterQueryString)
    {
        if (string.IsNullOrWhiteSpace(filterQueryString))
        {
            return approvalRequests;
        }

        var (filterQuery, parameters) = FilterQueryBuilder.CreateFilterQueryWithParameters<ApprovalRequest>(filterQueryString);
        var filteredApprovalRequests = approvalRequests.Where(filterQuery);

        return filteredApprovalRequests;
    }

    public static IQueryable<ApprovalRequest> Sort(this IQueryable<ApprovalRequest> approvalRequests, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
        {
            return approvalRequests.OrderBy(x => x.Status);
        }

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<ApprovalRequest>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
        {
            return approvalRequests.OrderBy(x => x.Status);
        }

        //ApprovalRequests.OrderBy(item => item.Name).ThenByDescending(item => item.Age);
        return approvalRequests.OrderBy(orderQuery);
    }

    // public static IQueryable<ApprovalRequest> IncludeAllRelatedData(this IQueryable<ApprovalRequest> approvalRequests)
    // {
    //     return approvalRequests.Include(x => x.Approver)
    //         .Include(x => x.LeaveRequest)
    //         .Include(x => x.Status);
    // }
}