using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using OutOfOffice.Identity.Service;
using OutOfOffice.Application.Contracts.Identity;
using OutOfOffice.Application.Models.Identity;
using OutOfOffice.Domain.ConfigurationModels;

namespace OutOfOffice.Identity;

public static class IdentityServiceRegistration
{
    public static IServiceCollection ConfigureIdentityService(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigurationStrToIdentity configurationStrToIdentity = configuration.GetSection(ConfigurationStrToIdentity.Key).Get<ConfigurationStrToIdentity>()!;
        
        services.AddDbContext<RepositoryIdentityDbContext>(options =>
            options.UseMySql(configurationStrToIdentity.SqlConnectionToIdentityDb, new MySqlServerVersion(new Version(8, 0, 26))));

        services.AddIdentity<User, IdentityRole>()
        .AddEntityFrameworkStores<RepositoryIdentityDbContext>()
        .AddDefaultTokenProviders();

        ConfigureJWT(configuration, services);        

        services.AddTransient<IAuthenticateService, AuthenticateService>();

        return services;
    }

    private static void ConfigureJWT(IConfiguration configuration, IServiceCollection services)
    {

        JwtConfiguration jwtConfiguration = configuration.GetSection(JwtConfiguration.Key).Get<JwtConfiguration>()!;
        services.AddAuthentication(opt => 
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfiguration.Issuer,
                ValidAudience = jwtConfiguration.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.SecretKey))
                
            };
        });
    }
}
