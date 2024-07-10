using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.API.Presentation.ActionFilters;
using OutOfOffice.Application.Features.Projects.Requests.Commands;
using OutOfOffice.Application.Features.Projects.Requests.Queries;
using OutOfOffice.Contracts.Infrastructure;
using OutOfOffice.Shared.DTOs.Project;
using OutOfOffice.Shared.RequestFeatures;

namespace OutOfOffice.API.Presentation.Controllers;

[ApiController]

[Route("api/projects")]
public class ProjectController : ControllerBase
{
    private readonly IMediator mediator;

    private readonly ILoggerManager logger;

    public ProjectController(IMediator mediator, ILoggerManager logger)
    {
        this.mediator = mediator;
        this.logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    [ServiceFilter(typeof(ExtractQueryAttribute))]
    public async Task<ActionResult<List<ProjectDto>>> GetProjectByParameters([FromQuery] ProjectParameters projectParameters)
    {
        projectParameters.FilterAndSearchTerm = HttpContext.Items["filterAndSearchTerm"]!.ToString() ?? string.Empty;
        var result = await mediator.Send(new GetProjectsByParamRequest { projectParameters = projectParameters });

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Item2));

        //return result.linkResponse.HasLinks ? Ok(result.linkResponse.LinkedEntities) : Ok(result.linkResponse.ShapedEntities);
        return Ok(result.Item1);
    }

    // [HttpGet]
    // public async Task<ActionResult<List<ProjectDto>>> GetProjects()
    // {
    //     var ProjectsDto = await mediator.Send(new GetProjectsRequest());

    //     return Ok(ProjectsDto);
    // }

    [HttpGet("{id:Guid}", Name = "GetProject")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<ProjectDto>> GetProject(Guid id)
    {
        var ProjectDto = await mediator.Send(new GetProjectByIdRequest { Id = id });

        return Ok(ProjectDto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesDefaultResponseType]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<ActionResult<ProjectDto>> CreateProject([FromBody] CreateProjectDto createProjectDto)
    {
        var projectDto = await mediator.Send(new CreateProjectCommand { ProjectDto = createProjectDto });

        return CreatedAtRoute("GetProject", new { projectDto.Id }, projectDto);
    }

    [HttpPut()]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectDto updateProjectDto)
    {
        await mediator.Send(new UpdateProjectCommand { ProjectDto = updateProjectDto });

        return NoContent();
    }

    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteProject(Guid id)
    {
        await mediator.Send(new DeleteProjectCommand { Id = id });

        return NoContent();
    }
}