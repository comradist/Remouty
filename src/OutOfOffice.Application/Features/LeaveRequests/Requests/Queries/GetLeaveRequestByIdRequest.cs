using MediatR;
using OutOfOffice.Shared.DTOs.LeaveRequest;

namespace OutOfOffice.Application.Features.LeaveRequests.Requests.Queries;

public class GetLeaveRequestByIdRequest : IRequest<LeaveRequestDto>
{
    public Guid Id { get; set; }
}