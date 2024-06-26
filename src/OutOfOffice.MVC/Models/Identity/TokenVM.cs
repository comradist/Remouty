namespace OutOfOffice.MVC.Models.Identity;

public class TokenVM
{
    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }

    public DateTime AccessTokenExpires { get; set; } = DateTime.Now.AddMinutes(60);

    public DateTime RefreshTokenExpires { get; set; } = DateTime.Now.AddDays(7);
} 
