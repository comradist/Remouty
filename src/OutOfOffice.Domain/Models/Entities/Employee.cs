using OutOfOffice.Domain.Models.Entities.LookUpTables;

namespace OutOfOffice.Domain.Models.Entities;

public class Employee
{
    public Guid ID { get; set; }

    public string FullName { get; set; }

    public int SubdivisionID { get; set; }
    public Subdivision Subdivision { get; set; }

    public int PositionID { get; set; }
    public Position Position { get; set; }

    public int StatusID { get; set; }
    public RequestStatus Status { get; set; }

    public Guid PeoplePartnerId { get; set; }
    public Employee PeoplePartner { get; set; }

    public int OutOfOfficeBalance { get; set; }

    public byte[] Photo { get; set; }
}