using MediatR;

namespace OutOfOffice.Application.Features.LeaveRequests.Requests.Commands;

public class DeleteLeaveRequestCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}