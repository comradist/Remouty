namespace OutOfOffice.Shared.RequestFeatures;

public class EmployeeParameters : RequestParameters
{
    public EmployeeParameters() => OrderBy = "name";

    public string? FullName { get; set; }

    public int? SubdivisionID { get; set; }

    public int? PositionID { get; set; }

    public int? StatusID { get; set; }

    public Guid? PeoplePartnerId { get; set; }

    public Guid? Id { get; set; }

    public int? OutOfOfficeBalance { get; set; }
}