using AutoMapper;
using MediatR;
using OutOfOffice.Application.Features.ApprovalRequests.Requests.Commands;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Domain.Models.Entities;

namespace OutOfOffice.Application.Features.ApprovalRequests.Handlers.Commands;

public class DeleteApprovalRequestCommandHandler : IRequestHandler<DeleteApprovalRequestCommand, Unit>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public DeleteApprovalRequestCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteApprovalRequestCommand request, CancellationToken cancellationToken)
    {
        var approvalRequest = await repositoryManager.ApprovalRequest.GetApprovalRequestByIdAsync(request.Id, false) ?? throw new Exception("ApprovalRequest not found");

        await repositoryManager.ApprovalRequest.DeleteAsync(approvalRequest);
        await repositoryManager.SaveChangesAsync();

        return Unit.Value;
    }
}