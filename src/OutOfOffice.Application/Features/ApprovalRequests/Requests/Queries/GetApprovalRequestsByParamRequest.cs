using MediatR;
using OutOfOffice.Shared.DTOs.ApprovalRequest;
using OutOfOffice.Shared.RequestFeatures;

namespace OutOfOffice.Application.Features.ApprovalRequests.Requests.Queries;

public class GetApprovalRequestsByParamRequest : IRequest<(List<ApprovalRequestDto>, MetaData)>
{
    public ApprovalRequestParameters ApprovalRequestParameters { get; set; }
}