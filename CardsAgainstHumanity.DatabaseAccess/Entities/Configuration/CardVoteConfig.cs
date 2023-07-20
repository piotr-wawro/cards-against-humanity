using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardsAgainstHumanity.DatabaseAccess.Entities.Configuration;

public class CardVoteConfig : IEntityTypeConfiguration<CardVote> {
    public void Configure(EntityTypeBuilder<CardVote> builder) {
        builder.Property(e => e.Vote)
            .HasColumnType("tinyint");

        if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development") {
            builder.HasData(
                new {
                    Vote = 1,
                    UserId = 1,
                    CardId = 1,
                },
                new {
                    Vote = 1,
                    UserId = 1,
                    CardId = 2,
                },
                new {
                    Vote = 0,
                    UserId = 2,
                    CardId = 2,
                }
            );
        }
    }
}
