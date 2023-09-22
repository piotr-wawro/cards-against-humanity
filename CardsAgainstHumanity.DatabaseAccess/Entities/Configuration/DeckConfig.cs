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
                new Deck {
                    Id = 1,
                    Name = "Core Game",
                    Language = Language.English,
                    UserId = 1,
                    black = 89,
                    white = 460,
                    SafeContent = false,
                }
            );
        }
    }
}
