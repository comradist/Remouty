using OutOfOffice.MVC.Services.Base;

namespace OutOfOffice.MVC.Models.Employee;

public class EmployeeVM
{
    public Guid Id { get; set; }

    public string FullName { get; set; }

    public Subdivision Subdivision { get; set; }

    public Position Position { get; set; }

    public RequestStatus Status { get; set; }

    public EmployeeDto? PeoplePartner { get; set; }

    public int OutOfOfficeBalance { get; set; }

    public byte[]? Photo { get; set; }
}