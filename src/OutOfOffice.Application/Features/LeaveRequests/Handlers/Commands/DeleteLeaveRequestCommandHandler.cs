using AutoMapper;
using MediatR;
using OutOfOffice.Application.Features.LeaveRequests.Requests.Commands;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Domain.Models.Entities;

namespace OutOfOffice.Application.Features.LeaveRequests.Handlers.Commands;

public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, Unit>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public DeleteLeaveRequestCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await repositoryManager.LeaveRequest.GetLeaveRequestByIdAsync(request.Id, false) ?? throw new Exception("LeaveRequest not found");

        await repositoryManager.LeaveRequest.DeleteAsync(leaveRequest);
        await repositoryManager.SaveChangesAsync();

        return Unit.Value;
    }
}