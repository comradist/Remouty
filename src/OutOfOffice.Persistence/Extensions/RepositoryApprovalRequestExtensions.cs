
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using OutOfOffice.Domain.Models.Entities;
using OutOfOffice.Persistence.Extensions.Utility;

namespace OutOfOffice.Persistence.Extensions;

public static class RepositoryApprovalRequestExtensions
{
    public static IQueryable<ApprovalRequest> FilterApprovalRequest(this IQueryable<ApprovalRequest> approvalRequests, uint min, uint max)
    {
        // var returnEmloyees = ApprovalRequests.Where(x => x.Age >= min && x.Age <= max);
        // if (!returnEmloyees.Any())
        // {
        //     throw new CollectionBySearchParamBadRequest($"Not found any ApprovalRequests between {min} and {max} parameters");
        // }
        // return returnEmloyees;
        throw new NotImplementedException();
    }

    public static IQueryable<ApprovalRequest> Search(this IQueryable<ApprovalRequest> approvalRequests, string searchVar)
    {
        if (string.IsNullOrWhiteSpace(searchVar))
        {
            return approvalRequests;
        }

        var lowerCaseTerm = searchVar.Trim().ToLower();

        var returnApprovalRequest = approvalRequests.Where(x => x.Status.Name.ToLower().Contains(lowerCaseTerm));
        if (!returnApprovalRequest.Any())
        {
            //throw new CollectionBySearchParamBadRequest($"Not found any ApprovalRequests with searched term {searchVar}");
            throw new NotImplementedException();
        }
        return returnApprovalRequest;
    }

    public static IQueryable<ApprovalRequest> Sort(this IQueryable<ApprovalRequest> approvalRequests, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
        {
            return approvalRequests.OrderBy(x => x.Status.Name);
        }

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<ApprovalRequest>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
        {
            return approvalRequests.OrderBy(x => x.Status.Name);
        }

        //ApprovalRequests.OrderBy(item => item.Name).ThenByDescending(item => item.Age);
        return approvalRequests.OrderBy(orderQuery);
    }

}