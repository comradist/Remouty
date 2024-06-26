using System.ComponentModel.DataAnnotations;

namespace OutOfOffice.MVC.Models.Identity;

public class UserAuthenticationVM
{
    [Required]
    [MaxLength(100)]
    [Display(Name = "Username or Email")]
    public string UserNameOrEmail { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public string? ReturnUrl { get; set; }
}