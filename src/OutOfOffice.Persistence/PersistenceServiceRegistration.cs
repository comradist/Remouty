using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OutOfOffice.Domain.ConfigurationModels;

namespace OutOfOffice.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigurationConStrToDbNote configurationConStrToDbNote = configuration.GetSection(ConfigurationConStrToDbNote.Key).Get<ConfigurationConStrToDbNote>();
        services.AddDbContext<RepositoryOutOfOfficeDbContext>(options =>
            options.UseMySql(configurationConStrToDbNote.SqlConnectionToAppDb, new MySqlServerVersion(new Version(8, 0, 26))));

        //services.AddScoped<INoteRepository, NoteRepository>();

        return services;
    }
}