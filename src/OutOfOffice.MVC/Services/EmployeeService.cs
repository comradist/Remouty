using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using AutoMapper;
using OutOfOffice.MVC.Contracts;
using OutOfOffice.MVC.Models.Employee;
using OutOfOffice.MVC.Services.Base;

namespace OutOfOffice.MVC.Services;

public class EmployeeService : BaseHttpService, IEmployeeService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;
    private JwtSecurityTokenHandler _tokenHandler;

    public EmployeeService(IClient client, IHttpContextAccessor httpContextAccessor,
        IMapper mapper)
        : base(client)
    {
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
        _tokenHandler = new JwtSecurityTokenHandler();
    }

    public async Task<EmployeeDto> GetEmployee(Guid id)
    {
        var employee = await _client.GetEmployeeAsync(id);
        return _mapper.Map<EmployeeDto>(employee);
    }


    public async Task<List<EmployeeVM>> GetAllEmployees()
    {
        var employees = await _client.EmployeesAllAsync();
        var employeesw = employees.ToList();
        return _mapper.Map<List<EmployeeVM>>(employeesw);
    }
}