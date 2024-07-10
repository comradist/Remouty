using MediatR;
using OutOfOffice.Shared.DTOs.Employee;
using OutOfOffice.Shared.DTOs.ApprovalRequest;

namespace OutOfOffice.Application.Features.ApprovalRequests.Requests.Commands;

public class CreateApprovalRequestCommand : IRequest<ApprovalRequestDto>
{
    public CreateApprovalRequestDto ApprovalRequestDto { get; set; }
}