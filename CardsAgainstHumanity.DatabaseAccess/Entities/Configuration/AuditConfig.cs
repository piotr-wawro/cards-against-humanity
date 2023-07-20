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
    }
}
