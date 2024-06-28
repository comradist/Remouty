
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutOfOffice.Domain.Models.Entities;
using OutOfOffice.Domain.Models.Entities.LookUpTables;

namespace OutOfOffice.Persistence.Configuration;

public static class SeedLookUpTable
{
    public static void LookUpTableConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AbsenceReason>().HasData(

            new AbsenceReason { Id = 1, Name = "Vacation" },
            new AbsenceReason { Id = 2, Name = "Sick Leave" },
            new AbsenceReason { Id = 3, Name = "Business Trip" },
            new AbsenceReason { Id = 5, Name = "Paternity Leave" },
            new AbsenceReason { Id = 4, Name = "Maternity Leave" },
            new AbsenceReason { Id = 6, Name = "Unpaid Leave" },
            new AbsenceReason { Id = 7, Name = "Other" }
        );

        modelBuilder.Entity<Position>().HasData(
            new Position { Id = 1, Name = "Junior Developer" },
            new Position { Id = 2, Name = "Middle Developer" },
            new Position { Id = 3, Name = "Senior Developer" },
            new Position { Id = 4, Name = "Team Lead" },
            new Position { Id = 5, Name = "HR Manager" },
            new Position { Id = 6, Name = "Project Manager" },
            new Position { Id = 7, Name = "Business Analyst" },
            new Position { Id = 8, Name = "QA Engineer" }
        );

        modelBuilder.Entity<ProjectStatus>().HasData(
            new ProjectStatus { Id = 1, Name = "In Progress" },
            new ProjectStatus { Id = 2, Name = "Completed" },
            new ProjectStatus { Id = 3, Name = "On Hold" },
            new ProjectStatus { Id = 4, Name = "Cancelled" }
        );

        modelBuilder.Entity<ProjectType>().HasData(
            new ProjectType { Id = 1, Name = "Internal" },
            new ProjectType { Id = 2, Name = "External" },
            new ProjectType { Id = 3, Name = "R&D" },
            new ProjectType { Id = 4, Name = "Maintenance" },
            new ProjectType { Id = 5, Name = "Development" }
        );

        modelBuilder.Entity<RequestStatus>().HasData(
            new RequestStatus { Id = 1, Name = "New" },
            new RequestStatus { Id = 2, Name = "Approved" },
            new RequestStatus { Id = 3, Name = "Rejected" },
            new RequestStatus { Id = 4, Name = "Cancelled" }
        );

        modelBuilder.Entity<Subdivision>().HasData(
            new Subdivision { Id = 1, Name = "Development" },
            new Subdivision { Id = 2, Name = "HR" },
            new Subdivision { Id = 3, Name = "QA" },
            new Subdivision { Id = 4, Name = "Management" }
        );
    }
}
