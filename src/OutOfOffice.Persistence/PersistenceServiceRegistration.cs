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
        services.AddDbContext<RepositoryNoteDbContext>(options =>
            options.UseSqlite(configurationConStrToDbNote.SqlConnectionToAppDb));

        services.AddScoped<INoteRepository, NoteRepository>();

        return services;
    }
}