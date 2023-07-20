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

    }
}
