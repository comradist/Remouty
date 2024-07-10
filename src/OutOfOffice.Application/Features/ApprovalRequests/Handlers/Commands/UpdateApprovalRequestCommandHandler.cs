using AutoMapper;
using MediatR;
using OutOfOffice.Application.Features.ApprovalRequests.Requests.Commands;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Domain.Models.Entities;

namespace OutOfOffice.Application.Features.ApprovalRequests.Handlers.Commands;

public class UpdateApprovalRequestCommandHandler : IRequestHandler<UpdateApprovalRequestCommand, Unit>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public UpdateApprovalRequestCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateApprovalRequestCommand request, CancellationToken cancellationToken)
    {
        var ApprovalRequest = mapper.Map<ApprovalRequest>(request.ApprovalRequestDto);

        await repositoryManager.ApprovalRequest.UpdateAsync(ApprovalRequest);
        await repositoryManager.SaveChangesAsync();

        return Unit.Value;

    }
}