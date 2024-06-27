using System.Text.Json;
using MediatR;
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
    [ServiceFilter(typeof(ExtractQueryAttribute))]
    public async Task<IActionResult> GetLeaveRequestByParameters([FromQuery] LeaveRequestParameters leaveRequestParameters)
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
    public async Task<ActionResult<LeaveRequestDto>> GetLeaveRequest(Guid id)
    {
        var LeaveRequestDto = await mediator.Send(new GetLeaveRequestByIdRequest { Id = id });

        return Ok(LeaveRequestDto);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<ActionResult<LeaveRequestDto>> CreateLeaveRequest([FromBody] CreateLeaveRequestDto createLeaveRequestDto)
    {
        var leaveRequestDto = await mediator.Send(new CreateLeaveRequestCommand { LeaveRequestDto = createLeaveRequestDto });

        return CreatedAtRoute("GetLeaveRequest", new { leaveRequestDto.Id }, leaveRequestDto);
    }

    [HttpPut()]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateLeaveRequest([FromBody] UpdateLeaveRequestDto updateLeaveRequestDto)
    {
        await mediator.Send(new UpdateLeaveRequestCommand { LeaveRequestDto = updateLeaveRequestDto });

        return NoContent();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteLeaveRequest(Guid id)
    {
        await mediator.Send(new DeleteLeaveRequestCommand { Id = id });

        return NoContent();
    }
}