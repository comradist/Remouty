using AutoMapper;
using MediatR;
using OutOfOffice.Application.Features.ApprovalRequests.Requests.Queries;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Shared.DTOs.ApprovalRequest;

namespace OutOfOffice.Application.Features.ApprovalRequests.Handlers.Queries;

public class GetApprovalRequestByIdRequestHandler : IRequestHandler<GetApprovalRequestByIdRequest, ApprovalRequestDto>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public GetApprovalRequestByIdRequestHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<ApprovalRequestDto> Handle(GetApprovalRequestByIdRequest request, CancellationToken cancellationToken)
    {
        var approvalRequest = await repositoryManager.ApprovalRequest.GetApprovalRequestByIdAsync(request.Id, false) ?? throw new Exception("ApprovalRequest not found");

        var approvalRequestDto = mapper.Map<ApprovalRequestDto>(approvalRequest);

        return approvalRequestDto;
    }
}