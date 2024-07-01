using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OutOfOffice.API.Presentation.ActionFilters;
using OutOfOffice.Application.Features.Employees.Requests.Commands;
using OutOfOffice.Application.Features.Employees.Requests.Queries;
using OutOfOffice.Contracts.Infrastructure;
using OutOfOffice.Shared.DTOs.Employee;
using OutOfOffice.Shared.RequestFeatures;

namespace OutOfOffice.API.Presentation.Controllers;

[ApiController]
[Authorize]
[Route("api/employees")]
public class EmployeeController : ControllerBase
{
    private readonly IMediator mediator;

    private readonly ILoggerManager logger;

    public EmployeeController(IMediator mediator, ILoggerManager logger)
    {
        this.mediator = mediator;
        this.logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    [ServiceFilter(typeof(ExtractQueryAttribute))]
    public async Task<ActionResult<List<EmployeeDto>>> GetEmployeesByParameters([FromQuery] EmployeeParameters employeeParameters)
    {
        employeeParameters.FilterAndSearchTerm = HttpContext.Items["filterAndSearchTerm"]!.ToString() ?? string.Empty;
        var result = await mediator.Send(new GetEmployeesByParamRequest { employeeParameters = employeeParameters });

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Item2));

        //return result.linkResponse.HasLinks ? Ok(result.linkResponse.LinkedEntities) : Ok(result.linkResponse.ShapedEntities);
        return Ok(result.Item1);
    }

    // [HttpGet]
    // public async Task<ActionResult<List<EmployeeDto>>> GetEmployees()
    // {
    //     var employeesDto = await mediator.Send(new GetEmployeesRequest());

    //     return Ok(employeesDto);
    // }

    [HttpGet("{id:Guid}", Name = "GetEmployee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<EmployeeDto>> GetEmployee(Guid id)
    {
        var employeeDto = await mediator.Send(new GetEmployeeByIdRequest { Id = id });

        return Ok(employeeDto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesDefaultResponseType]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<ActionResult<EmployeeDto>> CreateEmployee([FromBody] CreateEmployeeDto createEmployeeDto)
    {
        var employeeDto = await mediator.Send(new CreateEmployeeCommand { EmployeeDto = createEmployeeDto });

        return CreatedAtRoute("GetEmployee", new { employeeDto.Id }, employeeDto);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeDto updateEmployeeDto)
    {
        await mediator.Send(new UpdateEmployeeCommand { UpdateEmployeeDto = updateEmployeeDto });

        return NoContent();
    }

    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteEmployee(Guid id)
    {
        await mediator.Send(new DeleteEmployeeCommand { Id = id });

        return NoContent();
    }
}