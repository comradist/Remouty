using OutOfOffice.MVC.Shared.RequestFeatures;

namespace OutOfOffice.Shared.RequestFeatures;

public class EmployeeParameters : MetaData
{
    public Guid? Id { get; set; }

    public string? OrderBy { get; set; }

    public string? FullName { get; set; }

    public int? SubdivisionID { get; set; }

    public int? PositionID { get; set; }

    public int? StatusID { get; set; }

    public Guid? PeoplePartnerId { get; set; }

    public int? OutOfOfficeBalance { get; set; }
}