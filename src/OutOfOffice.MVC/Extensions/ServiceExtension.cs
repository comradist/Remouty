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
                options.LoginPath = "/Users/Login"; // Set the path for the login page
                options.LogoutPath = "/Account/Logout"; // Set the path for the logout page
            });
    }

    public static void ConfigureHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient<IClient, Client>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5000");
            });
    }
}