using System.Linq.Dynamic.Core;
using OutOfOffice.Domain.Models.Entities;
using OutOfOffice.Persistence.Extensions.Utility;

namespace OutOfOffice.Persistence.Extensions;

public static class RepositoryLeaveRequestExtensions
{
    public static IQueryable<LeaveRequest> FilterAndSearch(this IQueryable<LeaveRequest> leaveRequest, string filterQueryString)
    {
        if (string.IsNullOrWhiteSpace(filterQueryString))
        {
            return leaveRequest;
        }

        var (filterQuery, parameters) = FilterQueryBuilder.CreateFilterQueryWithParameters<LeaveRequest>(filterQueryString);
        var filteredLeaveRequest = leaveRequest.Where(filterQuery);

        return filteredLeaveRequest;
    }

    public static IQueryable<LeaveRequest> Sort(this IQueryable<LeaveRequest> leaveRequests, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
        {
            return leaveRequests.OrderBy(x => x.Status);
        }

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<LeaveRequest>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
        {
            return leaveRequests.OrderBy(x => x.Status);
        }

        //LeaveRequests.OrderBy(item => item.Name).ThenByDescending(item => item.Age);
        return leaveRequests.OrderBy(orderQuery);
    }
}