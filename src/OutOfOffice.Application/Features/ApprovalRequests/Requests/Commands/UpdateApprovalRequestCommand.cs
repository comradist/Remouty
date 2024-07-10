using MediatR;
using OutOfOffice.Shared.DTOs.Employee;
using OutOfOffice.Shared.DTOs.ApprovalRequest;

namespace OutOfOffice.Application.Features.ApprovalRequests.Requests.Commands;

public class UpdateApprovalRequestCommand : IRequest<Unit>
{
    public UpdateApprovalRequestDto ApprovalRequestDto { get; set; }
}