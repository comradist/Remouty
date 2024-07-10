using AutoMapper;
using MediatR;
using OutOfOffice.Shared.DTOs.LeaveRequest;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Application.Features.LeaveRequests.Requests.Queries;
using OutOfOffice.Shared.RequestFeatures;
namespace OutOfOffice.Application.Features.LeaveRequests.Handlers.Queries;

public class GetLeaveRequestsByParamRequestHandler : IRequestHandler<GetLeaveRequestsByParamRequest, (List<LeaveRequestDto>, MetaData)>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public GetLeaveRequestsByParamRequestHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<(List<LeaveRequestDto>, MetaData)> Handle(GetLeaveRequestsByParamRequest request, CancellationToken cancellationToken)
    {
        var pageListWithLeaveRequest = await repositoryManager.LeaveRequest.GetLeaveRequestsByParamAsync(request.LeaveRequestParameters, false) ?? throw new Exception("LeaveRequest not found");

        var leaveRequestsDto = mapper.Map<List<LeaveRequestDto>>(pageListWithLeaveRequest);

        return (leaveRequestsDto, pageListWithLeaveRequest.MetaData);
    }
}