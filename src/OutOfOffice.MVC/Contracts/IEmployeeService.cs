using OutOfOffice.MVC.Models.Employee;

namespace OutOfOffice.MVC.Contracts;

public interface IEmployeeService
{
    Task<List<EmployeeVM>> GetAllEmployees();
}