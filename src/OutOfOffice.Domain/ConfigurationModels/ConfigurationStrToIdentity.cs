namespace OutOfOffice.Domain.ConfigurationModels;

public class ConfigurationStrToIdentity
{
    public const string Key = "ConnectionStrings";

    public string? SqlConnectionToIdentityDb { get; set; }
}