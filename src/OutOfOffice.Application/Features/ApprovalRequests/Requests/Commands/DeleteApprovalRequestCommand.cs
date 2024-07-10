using MediatR;

namespace OutOfOffice.Application.Features.ApprovalRequests.Requests.Commands;

public class DeleteApprovalRequestCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}