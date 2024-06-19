using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyMind.Application.Contracts;
using MyMind.Application.Contracts.Infrastructure;
using MyMind.Application.Models.EmailEntities;
using MyMind.Domain.ConfigurationModels;
using MyMind.Infrastructure.Logger;
using MyMind.Infrastructure.Mail;
using NLog;

namespace MyMind.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigurationLogger configurationLogger = configuration.GetSection(ConfigurationLogger.Key).Get<ConfigurationLogger>();
        LogManager.Setup().LoadConfigurationFromFile(string.Concat(configurationLogger.PathToLog, configurationLogger.FileToLog));

        services.AddSingleton<ILoggerManager, LoggerManager>();

        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddTransient<IEmailSender, EmailSender>();
;
        return services;
    }
}