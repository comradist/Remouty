using AutoMapper;
using MediatR;
using OutOfOffice.Application.Features.LeaveRequests.Requests.Commands;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Domain.Models.Entities;
using OutOfOffice.Shared.DTOs.LeaveRequest;

namespace OutOfOffice.Application.Features.LeaveRequests.Handlers.Commands;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, LeaveRequestDto>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public CreateLeaveRequestCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<LeaveRequestDto> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = mapper.Map<LeaveRequest>(request.LeaveRequestDto);

        await repositoryManager.LeaveRequest.CreateAsync(leaveRequest);
        leaveRequest = await repositoryManager.LeaveRequest.GetLeaveRequestByIdAsync(leaveRequest.Id, false);

        var leaveRequestDto = mapper.Map<LeaveRequestDto>(leaveRequest);

        return leaveRequestDto;
    }
}
