using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace MyMind.Identity.ContextFactory;

public class RepositoryIdentityDbContextFactory : IDesignTimeDbContextFactory<RepositoryIdentityDbContext>
{
    // private readonly IOptionsMonitor<ConfigurationConStrToDbNote> options;

    // public RepositoryNoteDbContextFactory(IOptionsMonitor<ConfigurationConStrToDbNote> options)
    // {
    //     this.options = options;
    // }
    public RepositoryIdentityDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder().SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "MyMind.API"))
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<RepositoryIdentityDbContext>();
        builder.UseSqlite(configuration.GetConnectionString("SqlConnectionToIdentityDb"));


        return new RepositoryIdentityDbContext(builder.Options);
    }
}


