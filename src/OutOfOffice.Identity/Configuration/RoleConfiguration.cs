using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OutOfOffice.Identity.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
                new IdentityRole
                {
                    Id = "335308de-a56c-4a30-aea9-8702fdc4bc2b",
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                },
                new IdentityRole
                {
                    Id = "32884cd1-aaed-4131-bf49-f98f8c44d882",
                    Name = "HR Manager",
                    NormalizedName = "HR MANAGER"
                },
                new IdentityRole
                {
                    Id = "78915281-09ad-4fbc-befc-61b155f3ba3e",
                    Name = "Project Manager",
                    NormalizedName = "PROJECT MANAGER"
                },
                new IdentityRole
                {
                    Id = "cbc43a8e-f7bb-4445-baaf-1add431ffbbf",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                }
            );
    }
}