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
    }
}
