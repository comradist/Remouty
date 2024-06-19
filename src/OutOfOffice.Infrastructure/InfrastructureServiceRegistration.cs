using System.Reflection;
//using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyMind.Infrastructure.Logger;
using NLog;
using OutOfOffice.Application.Contracts.Infrastructure;
using OutOfOffice.Domain.ConfigurationModels;

namespace MyMind.Infrastructure;

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