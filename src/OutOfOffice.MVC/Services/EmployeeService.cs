using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using AutoMapper;
using Newtonsoft.Json;
using OutOfOffice.MVC.Configuration;
using OutOfOffice.MVC.Contracts;
using OutOfOffice.MVC.Models.Employee;
using OutOfOffice.MVC.Services.Base;
using OutOfOffice.MVC.Shared.RequestFeatures;
using OutOfOffice.Shared.RequestFeatures;

namespace OutOfOffice.MVC.Services;

public class EmployeeService : BaseHttpService, IEmployeeService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IMapper _mapper;

    private readonly LookUpTablesConfiguration lookUpTablesConfiguration;

    private JwtSecurityTokenHandler _tokenHandler;

    public EmployeeService(IClient client, IHttpContextAccessor httpContextAccessor,
        IMapper mapper, LookUpTablesConfiguration lookUpTablesConfiguration)
        : base(client)
    {
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
        this.lookUpTablesConfiguration = lookUpTablesConfiguration;
        _tokenHandler = new JwtSecurityTokenHandler();
    }

    public async Task<EmployeeDto> GetEmployee(Guid id)
    {
        var employee = await _client.GetEmployeeAsync(id);
        return _mapper.Map<EmployeeDto>(employee);
    }

    public async Task<EmployeeIndexVM> GetAllEmployeesAsync(EmployeeParameters employeeParameters)
    {

        var employeesAPIResponse = await _client.EmployeesAllAsync(employeeParameters.FullName, employeeParameters.SubdivisionID,
            employeeParameters.PositionID, employeeParameters.StatusID, employeeParameters.PeoplePartnerId, 
            employeeParameters.Id, employeeParameters.OutOfOfficeBalance, employeeParameters.CurrentPage, 
            employeeParameters.PageSize, filterAndSearchTerm: null, employeeParameters.OrderBy);

        var employeesVM = _mapper.Map<List<EmployeeVM>>(employeesAPIResponse.Result);
        var headerPagination = employeesAPIResponse.Headers["X-Pagination"].FirstOrDefault();
        if(string.IsNullOrEmpty(headerPagination))
        {
            throw new InvalidOperationException("X-Pagination header is missing.");
        }

        EmployeeIndexVM employeeIndexVM = new()
        {
            Employees = employeesVM,
            MetaData = JsonConvert.DeserializeObject<MetaData>(headerPagination),
            Subdivisions = lookUpTablesConfiguration.Subdivisions,
            Positions = lookUpTablesConfiguration.Positions,
            RequestStatuses = lookUpTablesConfiguration.RequestStatuses
        };
        
        return employeeIndexVM;
    }

    public async Task<EmployeeVM> CreateEmployeeAsync(CreateEmployeeVM employee)
    {
        var createEmployeeDto = _mapper.Map<CreateEmployeeDto>(employee);
        var employeeAPIResponse = await _client.EmployeesPOSTAsync(createEmployeeDto);

        var employeeVM = _mapper.Map<EmployeeVM>(employeeAPIResponse.Result);
        return employeeVM;
    }
}