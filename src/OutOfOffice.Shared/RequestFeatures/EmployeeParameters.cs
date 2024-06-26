namespace OutOfOffice.Shared.RequestFeatures;

public class EmployeeParameters : RequestParameters
{
    public EmployeeParameters() => OrderBy = "name";

    public string? FilterTerm { get; set; }

    public string? SearchTerm { get; set; }

    public string? SubdivisionId { get; set; }

    public string? PositionId { get; set; }

    public string? StatusId { get; set; }

}