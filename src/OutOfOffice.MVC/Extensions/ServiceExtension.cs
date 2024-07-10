using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using OutOfOffice.MVC.Services.Base;

namespace OutOfOffice.MVC.Extensions;

public static class ServiceExtension
{
    public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
            
                options.LoginPath = "/Identity/Login"; // Specify the login path
                options.AccessDeniedPath = "/Error/Unauthorized"; // Redirect here for unauthorized access
            });
    }

    public static void ConfigureHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient<IClient, Client>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5000");
            });
    }


    public static void ConfigureValidationFilterAttribute(this IServiceCollection services) =>
    services.AddScoped<CheckTokenExpirationAttribute>();
}