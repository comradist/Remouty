using System.ComponentModel.DataAnnotations;

namespace OutOfOffice.MVC.Models.Employee;

public class CreateEmployeeVM
{
    [Required]
    [MaxLength(50)]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "FullName must contain only letters and spaces")]
    public string FullName { get; set; }

    [Required]
    [RegularExpression(@"^(100|[1-9][0-9]?)$", ErrorMessage = "SubdivisionID must be between 1 and 100")]
    public int SubdivisionID { get; set; }

    [Required]
    [RegularExpression(@"^(100|[1-9][0-9]?)$", ErrorMessage = "PositionID must be between 1 and 100")]
    public int PositionID { get; set; }

    [Required]
    [RegularExpression(@"^(100|[1-9][0-9]?)$", ErrorMessage = "StatusID must be between 1 and 100")]
    public int StatusID { get; set; }

    public Guid? PeoplePartnerId { get; set; }

    [Required]
    [RegularExpression(@"^(100|[1-9][0-9]?)$", ErrorMessage = "OutOfOfficeBalance must be between 1 and 100")]
    public int OutOfOfficeBalance { get; set; }

    public byte[]? Photo { get; set; }
}