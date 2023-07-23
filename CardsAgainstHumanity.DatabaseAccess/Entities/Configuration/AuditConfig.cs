using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardsAgainstHumanity.DatabaseAccess.Entities.Configuration;

public class AuditConfig : IEntityTypeConfiguration<Audit> {
    public void Configure(EntityTypeBuilder<Audit> builder) {
        builder
            .HasOne(child => child.User)
            .WithMany(parent => parent.Audits)
            .HasForeignKey(child => child.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development") {
            builder.HasData(
                new Audit {
                    Id = 1,
                    AffectedOn = new DateTime(2021, 1, 1),
                    UserId = 1,
                },
                new Audit {
                    Id = 2,
                    AffectedOn = new DateTime(2021, 1, 2),
                    UserId = 1,
                },
                new Audit {
                    Id = 3,
                    AffectedOn = new DateTime(2021, 1, 3),
                    UserId = 2,
                }
            );
        }
    }
}
