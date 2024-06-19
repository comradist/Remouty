using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace OutOfOffice.Persistence;

public class RepositoryOutOfOfficeDbContext : DbContext
{
    public RepositoryOutOfOfficeDbContext(DbContextOptions<RepositoryOutOfOfficeDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryOutOfOfficeDbContext).Assembly);

        modelBuilder.Entity<Note>()
            .Property(x => x.LastModifiedData)
            .HasConversion<string>(
                x => x.ToString("yyyy-MM-dd HH:mm:ss"),
                x => DateTime.Parse(x));

        modelBuilder.Entity<Note>()
            .Property(x => x.DataCreated)
            .HasConversion<string>(
                x => x.ToString("yyyy-MM-dd HH:mm:ss"),
                x => DateTime.Parse(x));
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach(var entry in ChangeTracker.Entries<BaseDomainEntity>())
        {
            entry.Entity.LastModifiedBy = "Comrade";
            entry.Entity.LastModifiedData = DateTime.UtcNow;
            if(entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = "Comrade";
                entry.Entity.DataCreated = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<Note> Notes { get; set; }
}
