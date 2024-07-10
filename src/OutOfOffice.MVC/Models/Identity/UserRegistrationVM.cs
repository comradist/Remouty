using System.ComponentModel.DataAnnotations;

namespace OutOfOffice.MVC.Models.Identity;

public class UserRegistrationVM
{
    [Required]
    [MaxLength(50)]
    [Display(Name = "First Name")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "FullName must contain only letters and spaces")]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    [Display(Name = "Last Name")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "LastName must contain only letters and spaces")]
    public string LastName { get; set; }

    [Required]
    [MaxLength(20)]
    [Display(Name = "Username")]
    [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Username must contain only letters and numbers")]
    public string UserName { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Password must contain at least one uppercase, one lowercase, and one number.")]
    public string Password { get; set; }

    [Required]
    public string Role { get; set; }

    
    public List<Role>? Roles { get; set; }

    [Required]
    [MaxLength(100)]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }
}