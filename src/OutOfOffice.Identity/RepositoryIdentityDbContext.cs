using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyMind.Application.Models.Identity;
using MyMind.Identity.Configuration;

namespace MyMind.Identity;

public class RepositoryIdentityDbContext : IdentityDbContext<User>
{
    public RepositoryIdentityDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .Property(x => x.RefreshTokenExpiryTime)
            .HasConversion<string>(
                x => x.ToString("yyyy-MM-dd HH:mm:ss"),
                x => DateTime.Parse(x));

        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());

    }
}