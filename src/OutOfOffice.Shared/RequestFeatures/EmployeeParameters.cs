namespace OutOfOffice.Shared.RequestFeatures;

public class EmployeeParameters : RequestParameters
{
    public EmployeeParameters() => OrderBy = "name";

    public string? FullName { get; set; }

    public string? SubdivisionID { get; set; }

    public string? PositionID { get; set; }

    public string? StatusID { get; set; }

    public Guid? PeoplePartnerId { get; set; }

    public Guid? Id { get; set; }

    public int? OutOfOfficeBalance { get; set; }
}