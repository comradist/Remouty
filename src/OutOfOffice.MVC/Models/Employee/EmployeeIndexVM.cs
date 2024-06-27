using OutOfOffice.MVC.Models.Employee;
using OutOfOffice.MVC.Services.Base;
using OutOfOffice.MVC.Shared.RequestFeatures;

namespace OutOfOffice.MVC.Models.Employee;

public class EmployeeIndexVM{
    public List<EmployeeVM> Employees { get; set; }

    public MetaData MetaData { get; set; }

    public Guid? Id { get; set; }

    public string? FullName { get; set; }

    public int? SubdivisionId { get; set; }

    public int? PositionId { get; set; }

    public int? StatusId { get; set; }

    public Guid? PeoplePartnerId { get; set; }

    public List<Subdivision> Subdivisions { get; set; }

    public List<Position> Positions { get; set; }

    public List<RequestStatus> RequestStatuses { get; set; }

}