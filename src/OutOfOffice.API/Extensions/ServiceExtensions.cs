using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using OutOfOffice.API.Presentation.ActionFilters;
using OutOfOffice.Domain.ConfigurationModels;

namespace OutOfOffice.API.Extensions;

public static class ApiServiceRegistration
{
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo { Title = "OutOfOffice.Api", Version = "v1" });
        });
    }

    public static void ConfigureCQRS(this IServiceCollection services)
    {
        services.AddCors(parameter =>
        {
            parameter.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });
    }

    public static void ConfigureValidationFilterAttribute(this IServiceCollection services) =>
        services.AddScoped<ValidationFilterAttribute>();


    // public static OptionsBuilder<ConfigurationConStrToDbNote> ConfigureOptionsDbNote (this IServiceCollection services, IConfiguration configuration)
    // {
    //     return services.AddOptions<ConfigurationConStrToDbNote>()
    //                 .Bind(configuration.GetSection(ConfigurationConStrToDbNote.Key))
    //                 .ValidateDataAnnotations()
    //                 .ValidateOnStart();
    // }

    public static OptionsBuilder<JwtConfiguration> ConfigureOptionsJwt(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddOptions<JwtConfiguration>()
                    .Bind(configuration.GetSection(JwtConfiguration.Key))
                    .ValidateDataAnnotations()
                    .ValidateOnStart();
    }

    // public static OptionsBuilder<ConfigurationStrToIdentity> ConfigureOptionsIdentity(this IServiceCollection services, IConfiguration configuration)
    // {
    //     return services.AddOptions<ConfigurationStrToIdentity>()
    //                 .Bind(configuration.GetSection(ConfigurationStrToIdentity.Key))
    //                 .ValidateDataAnnotations()
    //                 .ValidateOnStart();
    // }

    // public static OptionsBuilder<ConfigurationLogger> ConfigureOptionsLogger(this IServiceCollection services, IConfiguration configuration)
    // {
    //     return services.AddOptions<ConfigurationLogger>()
    //                 .Bind(configuration.GetSection(ConfigurationLogger.Key))
    //                 .ValidateDataAnnotations()
    //                 .ValidateOnStart();
    // }

    // public static OptionsBuilder<T> ConfigureOptions<T>(this IServiceCollection services, IConfiguration configuration) where T : class
    // {
    //     return services.AddOptions<T>()
    //                 .Bind(configuration.GetSection(typeof(T).Name))
    //                 .ValidateDataAnnotations()
    //                 .ValidateOnStart();
    // }


}