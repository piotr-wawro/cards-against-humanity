using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardsAgainstHumanity.DatabaseAccess.Entities.Configuration;

public class UserConfig : IEntityTypeConfiguration<User> {
    public void Configure(EntityTypeBuilder<User> builder) {
        builder.Property(e => e.Nickname)
            .HasColumnType("nvarchar(64)");

        builder.Property(e => e.Email)
            .HasColumnType("nvarchar(64)");

        builder.Property(e => e.Hash)
            .HasColumnType("binary(32)");

        builder.Property(e => e.Salt)
            .HasColumnType("binary(16)");

        if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development") {
            builder.HasData(
                new {
                    Id = 1,
                    Nickname = "User1",
                    Email = "user1@xyz.com",
                    Hash = Enumerable.Repeat((byte)0x21, 32).ToArray(),
                    Salt = Enumerable.Repeat((byte)0x21, 16).ToArray(),
                    Created = new DateTime(2020, 11, 1),
                },
                new {
                    Id = 2,
                    Nickname = "User2",
                    Email = "user2@xyz.com",
                    Hash = Enumerable.Repeat((byte)0x22, 32).ToArray(),
                    Salt = Enumerable.Repeat((byte)0x22, 16).ToArray(),
                    Created = new DateTime(2020, 11, 2),
                }
            );
        }
    }
}
