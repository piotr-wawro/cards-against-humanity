using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardsAgainstHumanity.DatabaseAccess.Entities.Configuration;

public class DeckVoteConfig : IEntityTypeConfiguration<DeckVote> {
    public void Configure(EntityTypeBuilder<DeckVote> builder) {
        builder.Property(e => e.Vote)
            .HasColumnType("tinyint");
    }
}
