using AutoMapper;
using MediatR;
using OutOfOffice.Application.Features.LeaveRequests.Requests.Queries;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Shared.DTOs.LeaveRequest;

namespace OutOfOffice.Application.Features.LeaveRequests.Handlers.Queries;

public class GetLeaveRequestByIdRequestHandler : IRequestHandler<GetLeaveRequestByIdRequest, LeaveRequestDto>
{
    private readonly IRepositoryManager repositoryManager ;
    private readonly IMapper mapper;

    public GetLeaveRequestByIdRequestHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<LeaveRequestDto> Handle(GetLeaveRequestByIdRequest request, CancellationToken cancellationToken)
    {
        var leaveRequest = await repositoryManager.LeaveRequest.GetLeaveRequestByIdAsync(request.Id, false) ?? throw new Exception("LeaveRequest not found");

        var leaveRequestDto = mapper.Map<LeaveRequestDto>(leaveRequest);

        return leaveRequestDto;
    }
}