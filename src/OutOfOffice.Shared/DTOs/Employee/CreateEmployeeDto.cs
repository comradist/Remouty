using OutOfOffice.Shared.DTOs.Common;

namespace OutOfOffice.Shared.DTOs.Employee;

public class CreateEmployeeDto
{
    public string FullName { get; set; }

    public int SubdivisionID { get; set; }

    public int PositionID { get; set; }

    public int StatusID { get; set; }

    public Guid? PeoplePartnerId { get; set; }

    public int OutOfOfficeBalance { get; set; }

    public byte[]? Photo { get; set; }

    public ICollection<Guid>? ProjectIds { get; set; } = new List<Guid>();

}