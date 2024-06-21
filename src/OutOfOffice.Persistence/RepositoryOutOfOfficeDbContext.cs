using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OutOfOffice.Domain.Models.Entities;
using OutOfOffice.Domain.Models.Entities.LookUpTables;


namespace OutOfOffice.Persistence;

public class RepositoryOutOfOfficeDbContext : DbContext
{
    public RepositoryOutOfOfficeDbContext(DbContextOptions<RepositoryOutOfOfficeDbContext> options) : base(options)
    {

    }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<LeaveRequest> LeaveRequests { get; set; }

    public DbSet<ApprovalRequest> ApprovalRequests { get; set; }

    public DbSet<Project> Projects { get; set; }

    public DbSet<Position> Positions { get; set; }

    public DbSet<Subdivision> Subdivisions { get; set; }

    public DbSet<AbsenceReason> AbsenceReasons { get; set; }

    public DbSet<RequestStatus> RequestStatuses { get; set; }

    public DbSet<ProjectType> ProjectTypes { get; set; }

    public DbSet<ProjectStatus> ProjectStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryOutOfOfficeDbContext).Assembly);

        modelBuilder.Entity<LeaveRequest>()
            .Property(x => x.StartDate)
            .HasConversion<string>(
                x => x.ToString("yyyy-MM-dd HH:mm:ss"),
                x => DateTime.Parse(x));

        modelBuilder.Entity<LeaveRequest>()
            .Property(x => x.EndDate)
            .HasConversion<string>(
                x => x.ToString("yyyy-MM-dd HH:mm:ss"),
                x => DateTime.Parse(x));

        modelBuilder.Entity<Project>()
            .Property(x => x.StartDate)
            .HasConversion<string>(
                x => x.ToString("yyyy-MM-dd HH:mm:ss"),
                x => DateTime.Parse(x));

        modelBuilder.Entity<Project>()
            .Property(x => x.EndDate)
            .HasConversion<string>(
                x => x.HasValue ? x.Value.ToString("yyyy-MM-dd HH:mm:ss") : null,
                x => DateTime.Parse(x));

        // Employee configuration
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Subdivision)
            .WithMany()
            .HasForeignKey(e => e.SubdivisionId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Status)
            .WithMany()
            .HasForeignKey(e => e.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Position)
            .WithMany()
            .HasForeignKey(e => e.PositionId)
            .OnDelete(DeleteBehavior.Restrict);

        // LeaveRequest configuration
        modelBuilder.Entity<LeaveRequest>()
            .HasOne(lr => lr.AbsenceReason)
            .WithMany()
            .HasForeignKey(lr => lr.AbsenceReasonId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<LeaveRequest>()
            .HasOne(lr => lr.Status)
            .WithMany()
            .HasForeignKey(lr => lr.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        // ApprovalRequest configuration
        modelBuilder.Entity<ApprovalRequest>()
            .HasOne(ar => ar.Status)
            .WithMany()
            .HasForeignKey(ar => ar.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<ApprovalRequest>()
            .HasOne(ar => ar.LeaveRequest)
            .WithMany()
            .HasForeignKey(ar => ar.LeaveRequestId)
            .OnDelete(DeleteBehavior.Restrict);

        // Project configuration
        modelBuilder.Entity<Project>()
            .HasOne(p => p.ProjectType)
            .WithMany()
            .HasForeignKey(p => p.ProjectTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Project>()
            .HasOne(p => p.Status)
            .WithMany()
            .HasForeignKey(p => p.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    // public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    // {
    //     // foreach(var entry in ChangeTracker.Entries<BaseDomainEntity>())
    //     // {
    //     //     entry.Entity.LastModifiedBy = "Comrade";
    //     //     entry.Entity.LastModifiedData = DateTime.UtcNow;
    //     //     if(entry.State == EntityState.Added)
    //     //     {
    //     //         entry.Entity.CreatedBy = "Comrade";
    //     //         entry.Entity.DataCreated = DateTime.UtcNow;
    //     //     }
    //     // }

    //     return base.SaveChangesAsync(cancellationToken);
    // }
}
