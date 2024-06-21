namespace OutOfOffice.Shared.DTOs.Identity;

public class UserAuthenticationDto
{
    public string UserNameOrEmail { get; set; }

    public string Password { get; set; }
}