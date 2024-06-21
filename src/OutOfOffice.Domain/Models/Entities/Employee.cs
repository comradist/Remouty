using OutOfOffice.Domain.Models.Entities.Common;
using OutOfOffice.Domain.Models.Entities.LookUpTables;

namespace OutOfOffice.Domain.Models.Entities;

public class Employee : BaseEntity
{
    public string FullName { get; set; }

    public int SubdivisionId { get; set; }
    public Subdivision Subdivision { get; set; }

    public int PositionId { get; set; }
    public Position Position { get; set; }

    public int StatusId { get; set; }
    public RequestStatus Status { get; set; }

    public Guid PeoplePartnerId { get; set; }
    public Employee PeoplePartner { get; set; }

    public int OutOfOfficeBalance { get; set; }

    public byte[] Photo { get; set; }
}