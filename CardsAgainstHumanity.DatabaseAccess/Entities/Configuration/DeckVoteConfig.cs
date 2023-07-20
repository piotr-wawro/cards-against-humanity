using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardsAgainstHumanity.DatabaseAccess.Entities.Configuration;

public class DeckVoteConfig : IEntityTypeConfiguration<DeckVote> {
    public void Configure(EntityTypeBuilder<DeckVote> builder) {
        builder.Property(e => e.Vote)
            .HasColumnType("tinyint");

        if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development") {
            builder.HasData(
                new {
                    Vote = 1,
                    UserId = 1,
                    DeckId = 1,
                }
            );
        }
    }
}
