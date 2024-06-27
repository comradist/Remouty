using MediatR;
using OutOfOffice.Shared.DTOs.LeaveRequest;
using OutOfOffice.Shared.RequestFeatures;

namespace OutOfOffice.Application.Features.LeaveRequests.Requests.Queries;

public class GetLeaveRequestsByParamRequest : IRequest<(List<LeaveRequestDto>, MetaData)>
{
    public LeaveRequestParameters LeaveRequestParameters { get; set; }
}