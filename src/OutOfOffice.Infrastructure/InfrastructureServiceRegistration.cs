using System.Reflection;
//using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OutOfOffice.Infrastructure.Logger;
using NLog;
using OutOfOffice.Domain.ConfigurationModels;
using OutOfOffice.Contracts.Infrastructure;

namespace OutOfOffice.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigurationLogger configurationLogger = configuration.GetSection(ConfigurationLogger.Key).Get<ConfigurationLogger>();
        LogManager.Setup().LoadConfigurationFromFile(string.Concat(configurationLogger.PathToLog, configurationLogger.FileToLog));

        services.AddSingleton<ILoggerManager, LoggerManager>();

;
        return services;
    }
}