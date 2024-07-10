using MediatR;
using OutOfOffice.Shared.DTOs.Employee;
using OutOfOffice.Shared.DTOs.LeaveRequest;

namespace OutOfOffice.Application.Features.LeaveRequests.Requests.Commands;

public class UpdateLeaveRequestCommand : IRequest<Unit>
{
    public UpdateLeaveRequestDto LeaveRequestDto { get; set; }
}