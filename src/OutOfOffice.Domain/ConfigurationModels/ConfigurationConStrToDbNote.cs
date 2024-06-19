namespace OutOfOffice.Domain.ConfigurationModels;

public class ConfigurationConStrToDbNote
{
    public const string Key = "ConnectionStrings";
    
    public string? SqlConnectionToAppDb { get; set; }
}