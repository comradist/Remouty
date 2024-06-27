using AutoMapper;
using MediatR;
using OutOfOffice.Application.Features.LeaveRequests.Requests.Commands;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Domain.Models.Entities;

namespace OutOfOffice.Application.Features.LeaveRequests.Handlers.Commands;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public UpdateLeaveRequestCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = mapper.Map<LeaveRequest>(request.LeaveRequestDto);

        await repositoryManager.LeaveRequest.UpdateAsync(leaveRequest);
        await repositoryManager.SaveChangesAsync();

        return Unit.Value;

    }
}