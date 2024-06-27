using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OutOfOffice.API.Presentation.ActionFilters;
using OutOfOffice.Application.Features.ApprovalRequests.Requests.Commands;
using OutOfOffice.Application.Features.ApprovalRequests.Requests.Queries;
using OutOfOffice.Contracts.Infrastructure;
using OutOfOffice.Shared.DTOs.ApprovalRequest;
using OutOfOffice.Shared.RequestFeatures;

namespace OutOfOffice.API.Presentation.Controllers;

[ApiController]
[Route("api/ApprovalRequests")]
public class ApprovalRequestController : ControllerBase
{
    private readonly IMediator mediator;

    private readonly ILoggerManager logger;

    public ApprovalRequestController(IMediator mediator, ILoggerManager logger)
    {
        this.mediator = mediator;
        this.logger = logger;
    }

    [HttpGet]
    [ServiceFilter(typeof(ExtractQueryAttribute))]
    public async Task<IActionResult> GetApprovalRequestsByParameters([FromQuery] ApprovalRequestParameters approvalRequestParameters)
    {
        approvalRequestParameters.FilterAndSearchTerm = HttpContext.Items["filterAndSearchTerm"]!.ToString() ?? string.Empty;
        var result = await mediator.Send(new GetApprovalRequestsByParamRequest { ApprovalRequestParameters = approvalRequestParameters });

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Item2));

        //return result.linkResponse.HasLinks ? Ok(result.linkResponse.LinkedEntities) : Ok(result.linkResponse.ShapedEntities);
        return Ok(result.Item1);
    }

    // [HttpGet]
    // public async Task<ActionResult<List<ApprovalRequestDto>>> GetApprovalRequests()
    // {
    //     var ApprovalRequestsDto = await mediator.Send(new GetApprovalRequestsRequest());

    //     return Ok(ApprovalRequestsDto);
    // }

    [HttpGet("{id:Guid}", Name = "GetApprovalRequest")]
    public async Task<ActionResult<ApprovalRequestDto>> GetApprovalRequest(Guid id)
    {
        var ApprovalRequestDto = await mediator.Send(new GetApprovalRequestByIdRequest { Id = id });

        return Ok(ApprovalRequestDto);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<ActionResult<ApprovalRequestDto>> CreateApprovalRequest([FromBody] CreateApprovalRequestDto createApprovalRequestDto)
    {
        var approvalRequestDto = await mediator.Send(new CreateApprovalRequestCommand { ApprovalRequestDto = createApprovalRequestDto });

        return CreatedAtRoute("GetApprovalRequest", new { approvalRequestDto.Id }, approvalRequestDto);
    }

    [HttpPut]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateApprovalRequest([FromBody] UpdateApprovalRequestDto updateApprovalRequestDto)
    {
        await mediator.Send(new UpdateApprovalRequestCommand { ApprovalRequestDto = updateApprovalRequestDto });

        return NoContent();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteApprovalRequest(Guid id)
    {
        await mediator.Send(new DeleteApprovalRequestCommand { Id = id });

        return NoContent();
    }
}