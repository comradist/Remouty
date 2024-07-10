
using Microsoft.AspNetCore.Authentication.Cookies;
using OutOfOffice.MVC.Configuration;
using OutOfOffice.MVC.Contracts;
using OutOfOffice.MVC.Logger;
using OutOfOffice.MVC.Services;
using OutOfOffice.MVC.Services.Base;
using OutOfOffice.MVC.Extensions;
using NLog;
using OutOfOffice.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

LogManager.Setup().LoadConfigurationFromFile(builder.Configuration.GetConnectionString("PathToLog"));

builder.Services.ConfigureAuthentication(builder.Configuration);

builder.Services.AddControllersWithViews(options =>{
    options.Filters.Add<UserInfoFilter>();
});

builder.Services.AddRazorPages();

builder.Services.ConfigureHttpClient();

builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
builder.Services.AddTransient<IAuthenticateService, AuthenticateService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddSingleton<LookUpTablesConfiguration>();


builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseExceptionHandler("/Home/Error");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.UseRedirectMiddleware();

// var client = app.Services.GetRequiredService<IClient>();
// app.UseCheckTokenExpiration(client);

app.UseMiddleware<CheckTokenExpirationMiddleware>();

// app.MapWhen(context => context.User.Identity.IsAuthenticated, app =>
// {
//     app.Run(async context =>
//     {
//         context.Response.Redirect("/Home/Index");
//     });
// });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
