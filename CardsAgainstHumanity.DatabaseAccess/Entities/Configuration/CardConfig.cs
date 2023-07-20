using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardsAgainstHumanity.DatabaseAccess.Entities.Configuration;

public class CardConfig : IEntityTypeConfiguration<Card> {
    public void Configure(EntityTypeBuilder<Card> builder) {
        builder
            .HasOne(child => child.User)
            .WithMany(parent => parent.Cards)
            .HasForeignKey(child => child.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(e => e.Text)
            .HasColumnType("nvarchar(64)");

        builder.Property(e => e.Language)
            .HasColumnType("nvarchar(32)")
            .HasConversion<string>(
                e => e.ToString(),
                e => (Language)Enum.Parse(typeof(Language), e)
            );

        builder.Property(e => e.Type)
            .HasColumnType("nchar(5)")
            .HasConversion<string>(
                e => e.ToString(),
                e => (CardType)Enum.Parse(typeof(CardType), e)
            );

        if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development") {
            builder.HasData(
                new {
                    Id = 1,
                    Type = CardType.White,
                    Text = "White card 1",
                    Language = Language.English,
                    UserId = 1,
                },
                new {
                    Id = 2,
                    Type = CardType.White,
                    Text = "White card 2",
                    Language = Language.English,
                    UserId = 1,
                },
                new {
                    Id = 3,
                    Type = CardType.White,
                    Text = "White card 3",
                    Language = Language.English,
                    UserId = 2,
                },
                new {
                    Id = 4,
                    Type = CardType.White,
                    Text = "White card 4",
                    Language = Language.English,
                    UserId = 2,
                    BaseCardId = 2,
                },
                new {
                    Id = 5,
                    Type = CardType.Black,
                    Text = "Black card 1",
                    Language = Language.English,
                    UserId = 2,
                }
            );
        }
    }
}
