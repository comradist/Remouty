using AutoMapper;
using MediatR;
using OutOfOffice.Shared.DTOs.ApprovalRequest;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Application.Features.ApprovalRequests.Requests.Queries;
using OutOfOffice.Shared.RequestFeatures;
namespace OutOfOffice.Application.Features.ApprovalRequests.Handlers.Queries;

public class GetApprovalRequestsByParamRequestHandler : IRequestHandler<GetApprovalRequestsByParamRequest, (List<ApprovalRequestDto>, MetaData)>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public GetApprovalRequestsByParamRequestHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<(List<ApprovalRequestDto>, MetaData)> Handle(GetApprovalRequestsByParamRequest request, CancellationToken cancellationToken)
    {
        var pageListWithApprovalRequest = await repositoryManager.ApprovalRequest.GetApprovalRequestsByParamAsync(request.ApprovalRequestParameters, false) ?? throw new Exception("ApprovalRequest not found");

        var ApprovalRequestsDto = mapper.Map<List<ApprovalRequestDto>>(pageListWithApprovalRequest);

        return (ApprovalRequestsDto, pageListWithApprovalRequest.MetaData);
    }
}