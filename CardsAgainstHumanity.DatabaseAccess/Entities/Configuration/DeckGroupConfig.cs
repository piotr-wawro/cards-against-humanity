using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardsAgainstHumanity.DatabaseAccess.Entities.Configuration;

public class DeckGroupConfig : IEntityTypeConfiguration<DeckGroup> {
    public void Configure(EntityTypeBuilder<DeckGroup> builder) {
        builder.Property(e => e.Name)
            .HasColumnType("nvarchar(64)");


        if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development") {
            builder.HasData(
                new DeckGroup {
                    Id = 1,
                    Name = "User 1 deck group",
                    UserId = 1,
                    DeckId = 1,
                },
                new DeckGroup {
                    Id = 2,
                    Name = "User 2 deck group",
                    UserId = 2,
                    DeckId = 1,
                }
            );
        }
    }
}
