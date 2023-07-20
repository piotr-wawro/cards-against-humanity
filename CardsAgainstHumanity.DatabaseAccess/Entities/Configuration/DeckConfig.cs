using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardsAgainstHumanity.DatabaseAccess.Entities.Configuration;

public class DeckConfig : IEntityTypeConfiguration<Deck> {
    public void Configure(EntityTypeBuilder<Deck> builder) {
        builder
            .HasOne(child => child.User)
            .WithMany(parent => parent.Decks)
            .HasForeignKey(child => child.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(e => e.Name)
            .HasColumnType("nvarchar(64)");

        builder.Property(e => e.black)
            .HasColumnType("smallint");

        builder.Property(e => e.white)
            .HasColumnType("smallint");

        builder.Property(e => e.Language)
            .HasColumnType("nvarchar(32)")
            .HasConversion<string>(
                e => e.ToString(),
                e => (Language)Enum.Parse(typeof(Language), e)
            );

        if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development") {
            builder.HasData(
                new {
                    Id = 1,
                    Name = "Deck 1",
                    Language = Language.English,
                    UserId = 1,
                    black = 1,
                    white = 1,
                    safe_content = true,
                },
                new {
                    Id = 2,
                    Name = "Deck 2",
                    Language = Language.English,
                    UserId = 2,
                    black = 1,
                    white = 1,
                    safe_content = true,
                }
            );
        }
    }
}
