using MediatR;
using OutOfOffice.Shared.DTOs.ApprovalRequest;

namespace OutOfOffice.Application.Features.ApprovalRequests.Requests.Queries;

public class GetApprovalRequestByIdRequest : IRequest<ApprovalRequestDto>
{
    public Guid Id { get; set; }
}