using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardsAgainstHumanity.DatabaseAccess.Entities.Configuration;

public class CardVoteConfig : IEntityTypeConfiguration<CardVote> {
    public void Configure(EntityTypeBuilder<CardVote> builder) {
        builder.Property(e => e.Vote)
            .HasColumnType("tinyint");
    }
}
