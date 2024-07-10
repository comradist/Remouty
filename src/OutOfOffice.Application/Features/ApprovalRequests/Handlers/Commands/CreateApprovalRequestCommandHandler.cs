using AutoMapper;
using MediatR;
using OutOfOffice.Application.Features.ApprovalRequests.Requests.Commands;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Domain.Models.Entities;
using OutOfOffice.Shared.DTOs.ApprovalRequest;

namespace OutOfOffice.Application.Features.ApprovalRequests.Handlers.Commands;

public class CreateApprovalRequestCommandHandler : IRequestHandler<CreateApprovalRequestCommand, ApprovalRequestDto>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public CreateApprovalRequestCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<ApprovalRequestDto> Handle(CreateApprovalRequestCommand request, CancellationToken cancellationToken)
    {
        var approvalRequest = mapper.Map<ApprovalRequest>(request.ApprovalRequestDto);

        await repositoryManager.ApprovalRequest.CreateAsync(approvalRequest);
        approvalRequest = await repositoryManager.ApprovalRequest.GetApprovalRequestByIdAsync(approvalRequest.Id, false);

        var approvalRequestDto = mapper.Map<ApprovalRequestDto>(approvalRequest);

        return approvalRequestDto;
    }
}
