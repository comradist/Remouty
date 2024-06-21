using OutOfOffice.Domain.Models.Entities.LookUpTables;
using OutOfOffice.Shared.DTOs.Common;

namespace OutOfOffice.Shared.DTOs.Employee;

public class EmployeeDto : BaseDto
{
    public string FullName { get; set; }

    public Subdivision Subdivision { get; set; }

    public Position Position { get; set; }

    public RequestStatus Status { get; set; }

    public EmployeeDto PeoplePartner { get; set; }

    public int OutOfOfficeBalance { get; set; }

    public byte[] Photo { get; set; }
}