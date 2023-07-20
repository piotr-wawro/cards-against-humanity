using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardsAgainstHumanity.DatabaseAccess.Entities.Configuration;

public class UserHistoryConfig : IEntityTypeConfiguration<UserHistory> {
    public void Configure(EntityTypeBuilder<UserHistory> builder) {
        builder
            .HasOne(child => child.Audit)
            .WithOne(parent => parent.UserHistory)
            .HasForeignKey<Audit>();

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
                    Nickname = "User111",
                    Email = "user1@xyz.com",
                    Hash = Enumerable.Repeat((byte)0x21, 32).ToArray(),
                    Salt = Enumerable.Repeat((byte)0x21, 16).ToArray(),
                    UserId = 1,
                }, new {
                    Id = 2,
                    Nickname = "User22",
                    Email = "user2@xyz.com",
                    Hash = Enumerable.Repeat((byte)0x22, 32).ToArray(),
                    Salt = Enumerable.Repeat((byte)0x22, 16).ToArray(),
                    UserId = 2,
                }, new {
                    Id = 3,
                    Nickname = "User11",
                    Email = "user1@xyz.com",
                    Hash = Enumerable.Repeat((byte)0x21, 32).ToArray(),
                    Salt = Enumerable.Repeat((byte)0x21, 16).ToArray(),
                    UserId = 1,
                }
            );
        }
    }
}
