using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OutOfOffice.Persistence;

namespace OutOfOffice.Persistence.ContextFactory;

public class RepositoryAppDbContextFactory : IDesignTimeDbContextFactory<RepositoryOutOfOfficeDbContext>
{
    // private readonly IOptionsMonitor<ConfigurationConStrToDbNote> options;

    // public RepositoryNoteDbContextFactory(IOptionsMonitor<ConfigurationConStrToDbNote> options)
    // {
    //     this.options = options;
    // }

    public RepositoryOutOfOfficeDbContext CreateDbContext(string[] args)
    {
        // var builder = new DbContextOptionsBuilder<RepositoryNoteDbContext>();
        // builder.UseSqlite(options.CurrentValue.SqlConnectionToAppDb);

        var configuration = new ConfigurationBuilder().SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "OutOfOffice.API"))
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<RepositoryOutOfOfficeDbContext>();
        builder.UseMySql(configuration.GetConnectionString("SqlConnectionToAppDb"), new MySqlServerVersion(new Version(8, 0, 26)));


        return new RepositoryOutOfOfficeDbContext(builder.Options);
    }
}


