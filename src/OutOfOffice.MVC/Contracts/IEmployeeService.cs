using OutOfOffice.MVC.Models.Employee;
using OutOfOffice.Shared.RequestFeatures;

namespace OutOfOffice.MVC.Contracts;

public interface IEmployeeService
{
    Task<EmployeeIndexVM> GetAllEmployeesAsync(EmployeeParameters employeeParameters);
    Task<EmployeeVM> CreateEmployeeAsync(CreateEmployeeVM employee);
}