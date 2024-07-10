using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Domain.ConfigurationModels;
using OutOfOffice.Persistence.Repositories;

namespace OutOfOffice.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigurationConStrToDbNote configurationConStrToDbNote = configuration.GetSection(ConfigurationConStrToDbNote.Key).Get<ConfigurationConStrToDbNote>();
        services.AddDbContext<RepositoryOutOfOfficeDbContext>(options =>
            options.UseMySql(configurationConStrToDbNote.SqlConnectionToAppDb, new MySqlServerVersion(new Version(8, 0, 26))));

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IApprovalRequestRepository, ApprovalRequestRepository>();
        services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IRepositoryManager, RepositoryManager>();


        return services;
    }
}