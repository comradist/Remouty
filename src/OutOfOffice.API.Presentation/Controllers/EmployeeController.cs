using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OutOfOffice.API.Presentation.ActionFilters;
using OutOfOffice.Application.Feature.Request.Queries;
using OutOfOffice.Application.Features.Employees.Requests.Commands;
using OutOfOffice.Contracts.Infrastructure;
using OutOfOffice.Shared.DTOs.Employee;

namespace OutOfOffice.API.Presentation.Controllers;

[ApiController]
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
    public async Task<IActionResult> GetEmployees()
    {
        var employeesDto = await mediator.Send(new GetEmployeesRequest());

        return Ok(employeesDto);
    }

    [HttpGet("{id:Guid}", Name = "GetEmployee")]
    public async Task<IActionResult> GetEmployee(Guid id)
    {
        var employeeDto = await mediator.Send(new GetEmployeeByIdRequest { Id = id });

        return Ok(employeeDto);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto createEmployeeDto)
    {
        var employeeDto = await mediator.Send(new CreateEmployeeCommand { EmployeeDto = createEmployeeDto });

        return CreatedAtRoute("GetEmployee", new { employeeDto.Id }, employeeDto);
    }

    [HttpPut()]
    public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeDto updateEmployeeDto)
    {
        await mediator.Send(new UpdateEmployeeCommand { UpdateEmployeeDto = updateEmployeeDto });

        return NoContent();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteEmployee(Guid id)
    {
        await mediator.Send(new DeleteEmployeeCommand { Id = id });

        return NoContent();
    }
}