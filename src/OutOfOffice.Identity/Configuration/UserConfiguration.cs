using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OutOfOffice.Application.Models.Identity;

namespace OutOfOffice.Identity.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
    {
        var hasher = new PasswordHasher<User>();
        builder.HasData(
             new User
             {
                 Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                 Email = "employee@localhost.com",
                 NormalizedEmail = "EMPLOYEE@LOCALHOST.COM",
                 FirstName = "EmployeeFN",
                 LastName = "EmployeeLN",
                 UserName = "EmployeeTEST",
                 NormalizedUserName = "EMPLOYEETEST",
                 PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                 EmailConfirmed = true
             },
             new User
             {
                 Id = "9e224968-33e4-4652-b7b7-8574d048cdb9",
                 Email = "hrmanager@localhost.com",
                 NormalizedEmail = "HRMANAGER@LOCALHOST.COM",
                 FirstName = "HRManagerFN",
                 LastName = "HRManagerLN",
                 UserName = "HRManagerTEST",
                 NormalizedUserName = "HRMANAGERTEST",
                 PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                 EmailConfirmed = true
             },
             new User
             {
                 Id = "7f9a84bf-6557-48e5-ba2e-7c7276751a39",
                 Email = "projectmanager@localhost.com",
                 NormalizedEmail = "PROJECTMANAGER@LOCALHOST.COM",
                 FirstName = "ProjectManagerFN",
                 LastName = "ProjectManagerLN",
                 UserName = "ProjectManagerTEST",
                 NormalizedUserName = "PROJECTMANAGERTEST",
                 PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                 EmailConfirmed = true
             },
             new User
             {
                 Id = "026e9845-6dcd-4ff1-a8ba-74a889d7cc85",
                 Email = "administrator@localhost.com",
                 NormalizedEmail = "ADMINISTRATOR@LOCALHOST.COM",
                 FirstName = "AdministratorFN",
                 LastName = "AdministratorLN",
                 UserName = "AdministratorTEST",
                 NormalizedUserName = "ADMINISTRATORTEST",
                 PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                 EmailConfirmed = true
             }
        );
    }
}