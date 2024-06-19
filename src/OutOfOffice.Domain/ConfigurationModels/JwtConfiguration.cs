namespace OutOfOffice.Domain.ConfigurationModels;

public class JwtConfiguration
{
    public const string Key = "JwtConfiguration";
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }
    public TimeSpan TokenExpiration { get; set; }
}

