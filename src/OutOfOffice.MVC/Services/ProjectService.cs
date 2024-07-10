using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using AutoMapper;
using Newtonsoft.Json;
using OutOfOffice.MVC.Configuration;
using OutOfOffice.MVC.Contracts;
using OutOfOffice.MVC.Models.Project;
using OutOfOffice.MVC.Services.Base;
using OutOfOffice.MVC.Shared.RequestFeatures;
using OutOfOffice.Shared.RequestFeatures;

namespace OutOfOffice.MVC.Services;

public class ProjectService : BaseHttpService, IProjectService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IMapper _mapper;

    private readonly LookUpTablesConfiguration lookUpTablesConfiguration;

    private JwtSecurityTokenHandler _tokenHandler;

    public ProjectService(IClient client, IHttpContextAccessor httpContextAccessor,
        IMapper mapper, LookUpTablesConfiguration lookUpTablesConfiguration)
        : base(client)
    {
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
        this.lookUpTablesConfiguration = lookUpTablesConfiguration;
        _tokenHandler = new JwtSecurityTokenHandler();
    }

    public async Task<ProjectDto> GetProject(Guid id)
    {
        var Project = await _client.GetProjectAsync(id);
        return _mapper.Map<ProjectDto>(Project);
    }

    public async Task<ProjectIndexVM> GetAllProjectsAsync(ProjectParameters projectParameters)
    {

        var projectsAPIResponse = await _client.ProjectsAllAsync(projectParameters.Id, projectParameters.ProjectTypeId,
        projectParameters.StartDate, projectParameters.EndDate, projectParameters.ProjectManagerId,
        projectParameters.StatusId, projectParameters.CurrentPage, projectParameters.PageSize, 
        filterAndSearchTerm: null, projectParameters.OrderBy);


        var ProjectsVM = _mapper.Map<List<ProjectVM>>(projectsAPIResponse.Result);


        var headerPagination = projectsAPIResponse.Headers["X-Pagination"].FirstOrDefault();
        if (string.IsNullOrEmpty(headerPagination))
        {
            throw new InvalidOperationException("X-Pagination header is missing.");
        }

        ProjectIndexVM ProjectIndexVM = new()
        {
            Projects = ProjectsVM,
            MetaData = JsonConvert.DeserializeObject<MetaData>(headerPagination),
            ProjectStatuses = lookUpTablesConfiguration.ProjectStatuses,
            ProjectTypes = lookUpTablesConfiguration.ProjectTypes,
        };

        return ProjectIndexVM;
    }

    public async Task<ProjectVM> CreateProjectAsync(CreateProjectVM Project)
    {
        var createProjectDto = _mapper.Map<CreateProjectDto>(Project);
        var projectAPIResponse = await _client.ProjectsPOSTAsync(createProjectDto);

        var ProjectVM = _mapper.Map<ProjectVM>(projectAPIResponse.Result);
        return ProjectVM;
    }
}