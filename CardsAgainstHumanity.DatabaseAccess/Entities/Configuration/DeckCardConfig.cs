using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardsAgainstHumanity.DatabaseAccess.Entities.Configuration;

internal class DeckCardConfig : IEntityTypeConfiguration<DeckCard> {
    public void Configure(EntityTypeBuilder<DeckCard> builder) {
        if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development") {
            builder.HasData(
                new {
                    Id = 1,
                    DeckId = 1,
                    CardId = 1,
                },
                new {
                    Id = 2,
                    DeckId = 1,
                    CardId = 4,
                },
                new {
                    Id = 3,
                    DeckId = 2,
                    CardId = 3,
                },
                new {
                    Id = 4,
                    DeckId = 2,
                    CardId = 4,
                }
            );
        }
    }
}
