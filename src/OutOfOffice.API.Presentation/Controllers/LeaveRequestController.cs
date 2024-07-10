using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.API.Presentation.ActionFilters;
using OutOfOffice.Application.Features.LeaveRequests.Requests.Commands;
using OutOfOffice.Application.Features.LeaveRequests.Requests.Queries;
using OutOfOffice.Contracts.Infrastructure;
using OutOfOffice.Shared.DTOs.LeaveRequest;
using OutOfOffice.Shared.RequestFeatures;

namespace OutOfOffice.API.Presentation.Controllers;

[ApiController]

[Route("api/leaveRequests")]
public class LeaveRequestController : ControllerBase
{
    private readonly IMediator mediator;

    private readonly ILoggerManager logger;

    public LeaveRequestController(IMediator mediator, ILoggerManager logger)
    {
        this.mediator = mediator;
        this.logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    [ServiceFilter(typeof(ExtractQueryAttribute))]
    public async Task<ActionResult<List<LeaveRequestDto>>> GetLeaveRequestByParameters([FromQuery] LeaveRequestParameters leaveRequestParameters)
    {
        leaveRequestParameters.FilterAndSearchTerm = HttpContext.Items["filterAndSearchTerm"]!.ToString() ?? string.Empty;
        var result = await mediator.Send(new GetLeaveRequestsByParamRequest { LeaveRequestParameters = leaveRequestParameters });

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Item2));

        //return result.linkResponse.HasLinks ? Ok(result.linkResponse.LinkedEntities) : Ok(result.linkResponse.ShapedEntities);
        return Ok(result.Item1);
    }

    // [HttpGet]
    // public async Task<ActionResult<List<LeaveRequestDto>>> GetLeaveRequests()
    // {
    //     var LeaveRequestsDto = await mediator.Send(new GetLeaveRequestsRequest());

    //     return Ok(LeaveRequestsDto);
    // }

    [HttpGet("{id:Guid}", Name = "GetLeaveRequest")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<LeaveRequestDto>> GetLeaveRequest(Guid id)
    {
        var LeaveRequestDto = await mediator.Send(new GetLeaveRequestByIdRequest { Id = id });

        return Ok(LeaveRequestDto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesDefaultResponseType]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<ActionResult<LeaveRequestDto>> CreateLeaveRequest([FromBody] CreateLeaveRequestDto createLeaveRequestDto)
    {
        var leaveRequestDto = await mediator.Send(new CreateLeaveRequestCommand { LeaveRequestDto = createLeaveRequestDto });

        return CreatedAtRoute("GetLeaveRequest", new { leaveRequestDto.Id }, leaveRequestDto);
    }

    [HttpPut()]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateLeaveRequest([FromBody] UpdateLeaveRequestDto updateLeaveRequestDto)
    {
        await mediator.Send(new UpdateLeaveRequestCommand { LeaveRequestDto = updateLeaveRequestDto });

        return NoContent();
    }

    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteLeaveRequest(Guid id)
    {
        await mediator.Send(new DeleteLeaveRequestCommand { Id = id });

        return NoContent();
    }
}