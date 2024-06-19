namespace OutOfOffice.Domain.ConfigurationModels;

//! Not using this class
public class ConfigurationLogger
{
    public const string Key = "LoggerFolderPath";

    public string? PathToLog { get; set; }

    public string? FileToLog { get; set; }

}