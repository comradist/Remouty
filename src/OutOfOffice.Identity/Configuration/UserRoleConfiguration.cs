using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OutOfOffice.Identity.Configuration;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = "335308de-a56c-4a30-aea9-8702fdc4bc2b",
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
            },
            new IdentityUserRole<string>
            {

                RoleId = "32884cd1-aaed-4131-bf49-f98f8c44d882",
                UserId = "9e224968-33e4-4652-b7b7-8574d048cdb9"
            },
            new IdentityUserRole<string>
            {

                RoleId = "78915281-09ad-4fbc-befc-61b155f3ba3e",
                UserId = "7f9a84bf-6557-48e5-ba2e-7c7276751a39"
            },
            new IdentityUserRole<string>
            {

                RoleId = "cbc43a8e-f7bb-4445-baaf-1add431ffbbf",
                UserId = "026e9845-6dcd-4ff1-a8ba-74a889d7cc85"
            }
        );
    }
}