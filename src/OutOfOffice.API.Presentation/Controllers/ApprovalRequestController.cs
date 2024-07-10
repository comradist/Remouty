using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

[Route("api/approvalRequests")]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    [ServiceFilter(typeof(ExtractQueryAttribute))]
    public async Task<ActionResult<List<ApprovalRequestDto>>> GetApprovalRequestsByParameters([FromQuery] ApprovalRequestParameters approvalRequestParameters)
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<ApprovalRequestDto>> GetApprovalRequest(Guid id)
    {
        var ApprovalRequestDto = await mediator.Send(new GetApprovalRequestByIdRequest { Id = id });

        return Ok(ApprovalRequestDto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesDefaultResponseType]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<ActionResult<ApprovalRequestDto>> CreateApprovalRequest([FromBody] CreateApprovalRequestDto createApprovalRequestDto)
    {
        var approvalRequestDto = await mediator.Send(new CreateApprovalRequestCommand { ApprovalRequestDto = createApprovalRequestDto });

        return CreatedAtRoute("GetApprovalRequest", new { approvalRequestDto.Id }, approvalRequestDto);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateApprovalRequest([FromBody] UpdateApprovalRequestDto updateApprovalRequestDto)
    {
        await mediator.Send(new UpdateApprovalRequestCommand { ApprovalRequestDto = updateApprovalRequestDto });

        return NoContent();
    }

    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteApprovalRequest(Guid id)
    {
        await mediator.Send(new DeleteApprovalRequestCommand { Id = id });

        return NoContent();
    }
}