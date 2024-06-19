using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace MyMind.Persistence.ContextFactory;

public class RepositoryNoteDbContextFactory : IDesignTimeDbContextFactory<RepositoryNoteDbContext>
{
    // private readonly IOptionsMonitor<ConfigurationConStrToDbNote> options;

    // public RepositoryNoteDbContextFactory(IOptionsMonitor<ConfigurationConStrToDbNote> options)
    // {
    //     this.options = options;
    // }

    public RepositoryNoteDbContext CreateDbContext(string[] args)
    {
        // var builder = new DbContextOptionsBuilder<RepositoryNoteDbContext>();
        // builder.UseSqlite(options.CurrentValue.SqlConnectionToAppDb);

        var configuration = new ConfigurationBuilder().SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "MyMind.API"))
            .AddJsonFile("appsettings.json")
            .Build();
            

        var builder = new DbContextOptionsBuilder<RepositoryNoteDbContext>();
        builder.UseSqlite(configuration.GetConnectionString("SqlConnectionToAppDb"));


        return new RepositoryNoteDbContext(builder.Options);
    }
}


