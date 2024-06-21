namespace OutOfOffice.Shared.DTOs.Employee;

public interface IEmployeeDto
{
    public string FullName { get; set; }

    public int OutOfOfficeBalance { get; set; }
}